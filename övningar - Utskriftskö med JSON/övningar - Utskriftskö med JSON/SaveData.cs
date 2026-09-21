using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace övningar___Utskriftskö_med_JSON {
    internal class SaveData {
        public int NextId { get; set; }
        public List<PrintJob> jobs { get; set; } = new List<PrintJob>();
    }
}
