﻿namespace LMSApi2.DTOS.FilesDTO
{
    public class AddFileDTO
    {
        public string? FileName { get; set; }
        public byte[]? Data { get; set; }
        public string? MimeType { get; set; }

    }
}
