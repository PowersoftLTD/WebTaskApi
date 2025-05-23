﻿using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManagement.API.Model
{
    public class TASK_ACTION_TRL
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("PROGRESS_PERC")]
        public decimal? PROGRESS_PERC { get; set; }
        [JsonPropertyName("ACTION_TYPE")]
        public string? ACTION_TYPE { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public int? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("CURR_ACTION")]
        public string? CURR_ACTION { get; set; }
        [JsonPropertyName("RESPONSE_STATUS")]
        public string? RESPONSE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }
}
