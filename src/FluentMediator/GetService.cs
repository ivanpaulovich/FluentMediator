using System;

namespace FluentMediator
{
    /// <summary>
    /// Retrieves a Handler Service from the Container
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public delegate object GetService(Type serviceType);
}