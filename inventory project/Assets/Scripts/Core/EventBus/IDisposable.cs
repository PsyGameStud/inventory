namespace MongrelsTeam.Interfaces
{
    public interface IDisposable
    {
        /// <summary>
        /// В этом методе надо отписаться от всех ивентов сигнальной шины
        /// </summary>
        public void Dispose();
    }
}
