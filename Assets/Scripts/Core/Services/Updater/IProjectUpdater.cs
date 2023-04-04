using System;

namespace InputReader.Services.Updater
{
    public interface IProjectUpdater
    {
        event Action UpdateCalled;
        event Action FixedUpdateCalled;
        event Action LateUpdateCalled;
    }
}