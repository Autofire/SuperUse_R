using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A pooler for GameObjects. Disabled GameObjects are considered in the pool, so
/// make sure to enable GameObject as soon as they leave the pool.
/// </summary>
public sealed class GameObjectPooler : ObjectPooler<GameObject> {

	public bool parentPooledObjects = true;

	public GameObject pooledObject;

	protected override void Start() {
		UnityEngine.Assertions.Assert.IsNotNull(pooledObject);

		base.Start();
	}

	/// <summary>
	/// Creates a pool with the given properties, attaches it to targetObject, and returns the pool.
	/// </summary>
	/// <returns>The newly created pooler.</returns>
	/// <param name="targetObj">Object to attach the pool to.</param>
	/// <param name="pooledObject">Pooled object prefab.</param>
	/// <param name="pooledAmount">Amount of objects to initially put in the pool.</param>
	/// <param name="willGrow">If set to <c>true</c>, pool grows when empty.</param>
	/// <param name="parentPooledObjects">If set to <c>true</c>, pooled objects are parented to <c>targetObj</c>.</param>
	public static GameObjectPooler CreatePooler(
				GameObject targetObj,
				GameObject pooledObject,
				int initPoolSize,
				bool willGrow = true,
				bool parentPooledObjects = true)
	{
		GameObjectPooler newPooler = targetObj.AddComponent<GameObjectPooler>();

		newPooler.pooledObject = pooledObject;
		newPooler.initPoolSize = initPoolSize;
		newPooler.willGrow = willGrow;
		newPooler.parentPooledObjects = parentPooledObjects;

		newPooler.InitPool();

		return newPooler;
	}

	protected override GameObject CreateObject() {
		GameObject obj = (GameObject)Instantiate(
			pooledObject,
			(parentPooledObjects ? transform : null)
		);

		obj.SetActive(false);

		return obj;
	}

	protected override bool IsObjectInPool(GameObject obj) {
		return !obj.activeSelf;
	}
}