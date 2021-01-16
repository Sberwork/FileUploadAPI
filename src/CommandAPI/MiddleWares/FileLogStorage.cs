using System;
using Microsoft.Extensions.Configuration;

namespace CommandAPI.MiddleWares
{
    public class FileLogStorage : ILogStorage
    {

       string path = @"Middlewares\Updownloads\log.txt";
  
        public void Store(LogModel log)
        {
            string[] lines =
            {
                $"Path: {log.Path}",
                $"QueryString: {log.QueryString}",
                $"Method: {log.Method}",
                $"RequestBody: {log.RequestBody}",
                $"Requested at: {log.RequestedOn}",
                $"Response: {log.Response}",
                $"ResponseCode: {log.ResponseCode}",
                $"Responded at: {log.RespondedOn}",
                Environment.NewLine
            };

            System.IO.File.AppendAllLines(path, lines);
        }
    }

}