using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Reporting
{
    public class ReportGenerator
    {
        private readonly List<string> _lines = new();
        private int _blockedCount = 0;

        public void LogResult(string domain, bool success)
        {
            string status = success ? "OK" : "BLOCKED";
            if (!success) _blockedCount++;

            _lines.Add($" - {domain} .......... {status}");
        }

        public void GenerateAndOpenReport(int totalDomains)
        {

            List<string> report = new()
            {
                "=== DOMAIN EMULATOR TEST REPORT ===",
                $"Timestamp: {DateTime.Now}",
                "",
                $"Total domains tested: {totalDomains}",
                $"Blocked domains: {_blockedCount}",
                "",
                "Results:"
            };

        }
    }
}