
namespace xShared.Request
{
    public class ModelOperationRequest<T> : BaseRequest where T : class
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ModelOperationRequest()
        {            
        }

        /// <summary>
        /// Simple constructor with model instance argument.
        /// </summary>
        /// <param name="model"></param>
        public ModelOperationRequest(T model)
        {
            Model = model; 
        }

        public T Model { get; }
    }
}
