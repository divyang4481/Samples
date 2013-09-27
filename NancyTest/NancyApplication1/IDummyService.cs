namespace NancyApplication1
{
    public interface IDummyService
    {
        void Foo();
    }

    public class DummyService : IDummyService
    {
        public void Foo()
        {
            throw new System.NotImplementedException();
        }
    }
}