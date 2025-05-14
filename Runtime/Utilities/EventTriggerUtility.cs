using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

namespace  FireflyExtensions
{
    public static class EventTriggerUtility
    {
        public static void TriggerSystem<TTag>() where TTag : unmanaged, IComponentData
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            
            EntityQuery query = entityManager.CreateEntityQuery(typeof(TTag));
            
            if (!query.IsEmpty) return;
            
            entityManager.CreateEntity(typeof(TTag));
        }

        public static Entity  TriggerSystemWithData<TTag, TData>(TData data)
            where TTag : unmanaged, IComponentData
            where TData : unmanaged, IComponentData
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity entity = entityManager.CreateEntity(typeof(TTag), typeof(TData));
            entityManager.SetComponentData(entity, data);
            return entity;
        }
    }
}

