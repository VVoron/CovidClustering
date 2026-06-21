using NeuroCovid19.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroCovid19.Providers
{
    internal class AIAnalyzerProvider: IDisposable
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://127.0.0.1:8000";
        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(600);

        public AIAnalyzerProvider()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = RequestTimeout
            };
        }

        public AIAnalyzerProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(BaseUrl);
            }
            if (_httpClient.Timeout != RequestTimeout)
            {
                _httpClient.Timeout = RequestTimeout;
            }
        }

        public async Task AnalyzeClustersAsync(List<ClasterInfo> clasters)
        {
            var taskList = new List<Task>();

            foreach (var claster in clasters)
            {
                var csvData = ConvertToCsv(claster);

                var fullPrompt = $@"""
                Задача: Проанализируй предоставленный датасет с данными детей.
                Выполни детальный анализ, основанный на строгом сравнении слуховых показателей каждого ребенка с референтными значениями, зависящими от его возраста и срока гестации.

                Референтные значения для анализа:
                Отоакустическая эмиссия (ОАЭ):
                1) Возраст <= 3 мес.:
                - гестация <= 28 нед.: от 4.25 до 8.5 
                - гестация <= 32 нед.: от 6.5 до 9.75
                - гестация <= 36 нед.: от 7.0 до 9.25
                - гестация > 36 нед.: от 7.5 до 11.5
                2) Возраст 3-6 мес.:
                - гестация <= 28 нед.: от 7.4 до 11
                - гестация <= 32 нед.: от 8 до 10.5
                - гестация <= 36 нед.: от 8.5 до 10.5
                - гестация > 36 нед.: от 5.25 до 9.5
                3) Возраст > 6 мес.:
                - гестация <= 28 нед.: от 9 до 12.25
                - гестация <= 32 нед.: от 8 до 10.75
                - гестация <= 36 нед.: от 9 до 11
                - гестация > 36 нед.: от 7 до 10.5
                
                При прохождении обследований по ОАЭ использовались несколько частот, но не каждый ребенок прошел их все, из-за чего дополнительно для анализа предоставлена информация по количеству непройденных обследований и максимальный показатель.
                
                ASSR: Нормальным считается значение <= 25.

                Закономерности, на которые стоит обратить внимание и учесть в процессе анализа:
                1) Если ОАЭ средне или высокая (средняя по референтным значениям) и ASSR (средняя) - ниже или равна 25 дб и срок гестации меньше 33 недель - Необходимо еще 2 раза пройти исследования слуховой функций до 6 мес и в 1 год жизни
                2) Если ОАЭ средне или высокая (средняя по референтным значениям) и ASSR (средняя) - ниже или равна 25 дб и срок гестации 33+ недель (до 40 обычно) - Обследование необходимо пройти однократно в период до 9 мес
                3) Если ОАЭ низкоамплитудная (средняя по референтным значениям) и ASSR (средняя) - ниже или равна 25 дБ - необходимо провести исследование слуховой функции в период от 3 до 6, и 6 до 12 месяцев. Если слуховая функция улучшается или становиться прежней, то необходимо проверить слух через 1 год
                4) ОАЭ средняя или высокочастотная (средняя по референтным значениям) и ASSR (средняя) - выше 25 дБ и срок гестации до 36 недель - Необходимо еще 2 раз пройти исследования слуховой функций до 9 мес. При отсутствии улучшения или ухудшении показателей ASSR (пороги становятся еще выше), дети направляются в Сурдологический центр.
                5) ОАЭ средняя или высокочастотная (средняя по референтным значениям) и ASSR (средняя) - выше 25 дБ и срок гестации больше 37 недель - Необходимо еще 1 раз пройти исследования слуховой функций 6 мес и при отсутствии улучшения или ухудшении показателей ASSR (пороги становятся еще выше), он направляется в Сурдологический центр.
                6) ОАЭ низкоамплитудная (средняя по референтным значениям) и ASSR (средняя) - выше и ниже 25 дБ и срок гестации до 36 недель - Необходимо еще 2 раз пройти исследования слуховой функций до 9 мес. При отсутствии улучшения или ухудшении показателей ASSR (пороги становятся еще выше), дети направляются в Сурдологический центр.

                В процессе анализа необходимо учесть, снижение и стабилизацию слуха у детей. Например: 'В зависимости от возраста отмечается улучшение (ухудшение) показателей отоакустической эмиссии, и/или АССР.. За счёт увеличения (уменьшения, стабилизации) показателей отоакустической эмиссии, АССР'.

                В данных по каждому ребенку может присутствовать информация по возрасту на момент болезни, сроку беременности на момент болезни.
                Необходимо учесть данные параметры в процессе анализа.

                План анализа для каждого кластера:
                1) Классификация: Для каждого ребенка определи статус каждого показателя (Норма, Отклонение, Данные отсутствуют) на основе сравнения его данных с референтными значениями, соответствующими его возрасту и сроку гестации.
                2) Характеристика кластера: На основе классификации дай общую характеристику кластера. Является ли он условно-нормальным или проблемным? Опиши наиболее частые паттерны отклонений (например, 'преобладают нарушения ОАЭ на высоких частотах').
                3) Анализ состава: Опиши демографический портрет кластера (распределение по возрасту, сроку гестации). Есть ли доминирующая группа?
                4) Поиск зависимостей: Выяви и опиши взаимосвязи. Есть ли корреляция между отклонениями в слухе и параметрами ребенка (возраст, гестация)? Нарушения по одному показателю сочетаются с нарушениями по другому?
                5) Итоговый вывод: Сформулируй структурированное резюме по кластеру: его условное название, ключевые findings и гипотезу о том, какую общую характеристику или проблему этот кластер иллюстрирует. Представь ответ в виде четкого отчета.

                Важно описать о необходимостях проведения исследований слуха, направлении в Сурдологический центр.

                Если в значениях возраста на момент болезни стоит 0 или 'не число', значит ребенок не болел либо мать не болела ковидом.

                В ответе представь информацию в кратком виде по блокам:
                1) Характеристика
                2) Частые паттерны и зависимости
                3) Тенденции по улучшению и стабилизации
                4) Итоговый вывод (динамика развития, группы риска, требующие мониторинг с % вошедших туда детей)

                *Важно* Не описывай процесс мышления и оценки. Не пиши таблицы. Описывай текстом по блокам.
                *Важно* Не описывай отдельно каждого обследуемого, нужен общий анализ по всей выборке.

                Данные выборки в табличном виде:
                {csvData}
                """;

                taskList.Add(SendToAiAsync(fullPrompt, claster));
            }

            await Task.WhenAll(taskList);
        }

        private string ConvertToCsv(ClasterInfo claster)
        {
            if (claster.Items.Length == 0) return string.Empty;

            var sb = new StringBuilder();

            var (properties, elements) = claster.GetDataForAI();

            sb.AppendLine(string.Join(";", properties));

            foreach (var point in elements)
            {
                sb.AppendLine(string.Join(";", point));
            }

            return sb.ToString();
        }

        private async Task<string> SendToAiAsync(string prompt, ClasterInfo claster)
        {
            try
            {
                var requestBody = new AnalyzeRequest { Prompt = prompt };
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync("/api/analyze", jsonContent);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AnalyzeResponse>(responseBody);

                if (result != null && result.Success)
                {
                    claster.DeepseekAnalysis = result.Response;
                    return result.Response;
                }

                var errorMessage = result?.Error ?? "Неизвестная ошибка при анализе кластера";
                claster.DeepseekAnalysis = $"Ошибка анализа: {errorMessage}";
                return $"Error: {errorMessage}";
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = $"Сервер анализа недоступен ({BaseUrl}/api/analyze): {ex.Message}";
                claster.DeepseekAnalysis = errorMsg;
                return $"Error: {errorMsg}";
            }
            catch (TaskCanceledException)
            {
                var errorMsg = $"Превышен таймаут ожидания ответа от сервера анализа ({RequestTimeout.TotalSeconds} сек)";
                claster.DeepseekAnalysis = errorMsg;
                return $"Error: {errorMsg}";
            }
            catch (JsonException ex)
            {
                var errorMsg = $"Ошибка обработки ответа от сервера анализа: {ex.Message}";
                claster.DeepseekAnalysis = errorMsg;
                return $"Error: {errorMsg}";
            }
            catch (Exception ex)
            {
                var errorMsg = $"Неожиданная ошибка при анализе кластера: {ex.Message}";
                claster.DeepseekAnalysis = errorMsg;
                return $"Error: {errorMsg}";
            }
        }

        public async Task<bool> CheckHealthAsync()
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var response = await _httpClient.GetAsync("/api/health", cts.Token);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    internal class AnalyzeRequest
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;
    }

    internal class AnalyzeResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("response")]
        public string? Response { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
