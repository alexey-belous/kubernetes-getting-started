namespace Minikube.Registration
{
    public class Maybe<T>
    {
        public bool HasError { get; }
        public T Result { get; }
        public string Error { get; }

        public Maybe(T result)
        {
            HasError = false;
            Result = result;
        }

        public Maybe(string error)
        {
            HasError = true;
            Error = error;
        }
    }
}