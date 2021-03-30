namespace NETCore.IntegrationTesting.WebAPI.Diagnostics
{
    public class MetricRecorder : IMetricRecorder
    {
        public void RecordMetric(string name, int increment, string[] tags)
        {
            // does nothing for this demo, you can add datadog, prometheus or appinsights logic here
        }
    }
}