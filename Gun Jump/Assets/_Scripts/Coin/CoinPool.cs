using System;
using System.Collections.Generic;
using _Scripts.Misc;
using UnityEngine;

namespace _Scripts.Coin
{
    [DisallowMultipleComponent]
    public class CoinPool : Singleton<CoinPool>
    {
        [SerializeField] private Coin _coinPrefab;

        private Queue<Coin> _coinPool = new();

        protected override void Awake()
        {
            base.Awake();
        }

        public Coin GetPooledObject()
        {
            if (_coinPool.Count == 0)
                AddCoin(10);

            Coin coin = _coinPool.Dequeue();
            coin.gameObject.SetActive(true);

            return coin;
        }

        public void ReturnToPool(Coin coin)
        {
            coin.gameObject.SetActive(false);
            _coinPool.Enqueue(coin);
        }
    
        private void AddCoin(int count)
        {
            if (count <= 0)
                throw new ArgumentException("The count value cannot be less than or equal to zero.");

            for (int i = 0; i < count; i++)
            {
                Coin coin = Instantiate(_coinPrefab);
                coin.gameObject.SetActive(false);
                _coinPool.Enqueue(coin);
            }
        }
    }
}
