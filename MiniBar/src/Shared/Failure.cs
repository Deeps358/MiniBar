using System.Collections;

namespace Shared
{
    public class Failure : IEnumerable<Error>
    {
        private readonly List<Error> _errors;

        public Failure(IEnumerable<Error> errors)
        {
            _errors = [..errors]; // полностью копирует, а не присваивает ссылку
        }

        public IEnumerator<Error> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static implicit operator Failure(Error[] errors)
            => new(errors);

        public static implicit operator Failure(Error error)
            => new([error]); // когда примет на вход ошибку, то преобразует к Failure в массив с одной ошибкой
    }
}
