using System;

namespace MasterPI2
{
	public class Lazy<T>
    {
    	private bool invalidated = true;
    	private T value;
    	private Func<T> initializer;
    
    	public Lazy(Func<T> initializer)
    	{
    		this.initializer = initializer;
    	}
    
    	private T Fetch()
    	{
    		value = initializer();
    		invalidated = false;
    		return value;
    	}
    
    	public void Invalidate() => invalidated = true;
    	public T Value => invalidated ? Fetch() : value;
    }
}

