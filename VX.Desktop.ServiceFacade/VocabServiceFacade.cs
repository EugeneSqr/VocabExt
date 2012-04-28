using VX.Desktop.ServiceFacade.VocabServiceReference;

namespace VX.Desktop.ServiceFacade
{
    public class VocabServiceFacade : IVocabServiceFacade
    {
        private readonly IVocabExtService service = new VocabExtServiceClient();

        public TaskContract GetTask()
        {
            return (TaskContract)service.GetTask();
        }
    }
}
