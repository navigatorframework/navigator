using System;
using System.Threading.Tasks;
using Navigator.Entities;

namespace Navigator.Context;

public interface INavigatorContextBuilder
{
    Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> configurationAction);
}