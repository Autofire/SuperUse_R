using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Originally from https://unity3d.com/learn/tutorials/topics/scripting/object-pooling
// Copied by http://answers.unity3d.com/answers/768109/view.html
// Lazy derp derp

/// <summary>
/// This is the base class for any pooler of objects.
/// </summary>
public abstract class ObjectPooler<TObj> : MonoBehaviour
	where TObj : Object
{
	/// <summary>
	/// 
	/// </summary>
	public delegate void ObjectRepooler(TObj reclaimedObj);
	private class pooledObject {

		public TObj obj { get { return _obj; } }
		public ObjectRepooler returnToPool;

		private TObj _obj;

		public pooledObject(TObj newObj) {
			_obj = newObj;
			returnToPool = null;
		}
	}


	public int initPoolSize = 0;
	public bool willGrow = true;

	private List<pooledObject> pool;

	protected virtual void Start() {
		// If the pool has been externally contructed and it has already been
		// initialized, there's no point in doing it again.
		if(pool == null) {
			InitPool();
		}
	}

	/// <summary>
	/// Initializes the pool, first deleting all objects contained in it and then
	/// filling it with pooledAmount of pooledObjects.
	/// </summary>
	public void InitPool() {
		if(pool != null) {
			foreach(pooledObject pooledObj in pool) {
				Destroy(pooledObj.obj);
			}
		}

		pool = new List<pooledObject>();
		for(int i = 0; i < initPoolSize; i++) {
			AddObjectToPool();
		}
	}

	/// <summary>
	/// Gets an object out of the pool. Depending on the pool's configuration,
	/// THIS COULD BE NULL!
	/// 
	/// Furthermore, the retrieved object should be used/activated immediately after
	/// calling this function. (What that means is up to the pooler; check its
	/// documentation.) Otherwise, subsequent method calls may yield control of the
	/// pooled object to someone else.
	/// 
	/// Also, it's a bad idea to maintain a reference to a pooled object after it
	/// becomes inactive. Use repoolDelegate if you plan on maintaining a reference
	/// to the given object beyond the scope of one function.
	/// </summary>
	///
	/// <returns>One of the objects ouf of the pool or null.</returns>
	///
	/// <param name="repoolDelegate">
	/// Optional delegate which gets called as soon as the object is called up again.
	/// Please avoid calling GetPooledObject within the delegate.
	/// </param>
	public TObj GetPooledObject(ObjectRepooler repoolDelegate = null) {
		pooledObject grabbedObj = null;

		int index = 0;

		while(index < pool.Count && grabbedObj == null) {
			if(pool[index].obj == null) {
				// One of the elements of the list is null; repopulate that element.
				grabbedObj = new pooledObject(CreateObject());
				pool[index] = grabbedObj;
			}
			else if(IsObjectInPool(pool[index].obj)) {
				grabbedObj = pool[index];
			} 
			else {
				index++;
			}
		}

		if(grabbedObj == null && willGrow) {
			grabbedObj = AddObjectToPool();
		}

		if(grabbedObj.returnToPool != null) {
			grabbedObj.returnToPool(grabbedObj.obj);
		}

		grabbedObj.returnToPool = repoolDelegate;

		return grabbedObj.obj;
	}

	private pooledObject AddObjectToPool() {
		pooledObject newObj = new pooledObject(CreateObject());

		pool.Add(newObj);
		return newObj;
	}

	protected abstract TObj CreateObject();
	protected abstract bool IsObjectInPool(TObj obj);
}
