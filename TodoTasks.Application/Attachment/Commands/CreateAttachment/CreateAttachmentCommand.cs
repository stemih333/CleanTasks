﻿using TodoTasks.Application.BaseClasses;
using MediatR;
using System.IO;

namespace TodoTasks.Application.Attachment.Commands
{
    public class CreateAttachmentCommand : BaseCommand, IRequest<int>
    {
        public Stream FileStream { get; set; } 
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string Description { get; set; }
        public int? TodoId { get; set; }
    }
}
