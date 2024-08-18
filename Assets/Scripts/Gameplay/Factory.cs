
    using UnityEngine;

    public class Factory<FactoryObject> : MonoBehaviour where FactoryObject : MonoBehaviour

    {
        [SerializeField] private FactoryObject prefab;

        public FactoryObject GetNewInstance()
        {
            return Instantiate(prefab);
        }
    }
    