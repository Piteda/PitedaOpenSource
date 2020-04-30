using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PitedaBarcodes;

namespace Piteda
{
    
    class MainClass
    {
        // Create an instanciate of the log class from the PitedaBarcodes.dll
        private LOG _log = new LOG();
        private ReadBarcodes ReadBarCodes = new ReadBarcodes();

        public List<Tuple<int, string>> res_list = new List<Tuple<int, string>>();

        public void Read(Bitmap src_img)
        {
            /// Create a Bitmap 
            Tuple<int, string> barcode_result = ReadBarCodes.ReadSingleBarcode(src_img);
            if (barcode_result.Item2 != null  && barcode_result.Item2 != _log.PACK_SIZE_ERROR && barcode_result.Item2 != _log.BARCODE_AS_BEEN_READ_MSG)
            {
                res_list.Add(barcode_result);
            }
        }

        public string GetErrorTest()
        {
             string error = _log.PACK_SIZE_ERROR;
             return error;
        }
    }
}
