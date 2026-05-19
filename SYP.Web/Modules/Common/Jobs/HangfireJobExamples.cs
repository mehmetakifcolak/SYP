using Hangfire;
using System;

namespace SYP.Common.Jobs;

/// <summary>
/// Hangfire background job örnekları
/// </summary>
public class HangfireJobExamples
{
    /// <summary>
    /// Basit bir fire-and-forget job örneği
    /// Anında çalışır
    /// </summary>
    public static void FireAndForgetJobExample()
    {
        BackgroundJob.Enqueue(() => Console.WriteLine("Fire and Forget Job çalıştı: " + DateTime.Now));
    }

    /// <summary>
    /// Belirli bir süre sonra çalışacak job
    /// </summary>
    public static void DelayedJobExample(TimeSpan delay)
    {
        BackgroundJob.Schedule(
            () => Console.WriteLine("Delayed Job çalıştı: " + DateTime.Now),
            delay);
    }

    /// <summary>
    /// Tekrarlı job örneği (Her 1 saat sonra)
    /// </summary>
    public static void RecurringJobExample()
    {
        RecurringJob.AddOrUpdate(
            "recurring-job-example",
            () => Console.WriteLine("Recurring Job çalıştı: " + DateTime.Now),
            Cron.Hourly);
    }

    /// <summary>
    /// Yapılacak iş örneği
    /// </summary>
    [Queue("default")]
    public static void ProcessData(string data)
    {
        System.Diagnostics.Debug.WriteLine($"Processing data: {data} at {DateTime.Now}");
    }

    /// <summary>
    /// Hata handling ile job
    /// </summary>
    [Queue("default")]
    [AutomaticRetry(Attempts = 3)]
    public static void ProcessWithRetry(string jobId)
    {
        System.Diagnostics.Debug.WriteLine($"Processing job {jobId} with retry at {DateTime.Now}");
    }
}
