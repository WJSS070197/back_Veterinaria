﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIG_VETERINARIA.Dtos.Common
{
    public class ResultDto<T>
    {
        public string Message {  get; set; }
        public string MessegeException {  get; set; }
        public Boolean IsSuccess { get; set; }
        public T Item  { get; set; }
        public List<T> Data { get; set; }
    }
}
