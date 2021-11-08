

namespace MansionArroz.Model
{
    public class JsonResponse
    {
        public object Data { set; get; }

        public bool Result { get; set; }

        public JsonControl Control { get; set; }

        public JsonResponse()
        {
            Control = new JsonControl();
            Data = null;
            Result = false;
            Control.Code = "400";
            Control.Show = false;
            Control.Message = "Error";
            Control.AlertType = "Error";
        }
    }
}
