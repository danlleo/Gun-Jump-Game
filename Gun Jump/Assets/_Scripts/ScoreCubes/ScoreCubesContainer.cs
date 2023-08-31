using System.Collections.Generic;
using UnityEngine;

public class ScoreCubesContainer : MonoBehaviour
{
    [SerializeField] private List<ScoreCube> _scoreCubeList;

    private void Awake()
    {
        PlaceCubesInRandomOrder();
    }

    private void PlaceCubesInRandomOrder()
    {
        Vector3 cubeTopPosition = Vector3.zero;
        int spawnedCubesCount = 0;

        while (_scoreCubeList.Count > 0)
        {
            int randomCubeIndex = Random.Range(0, _scoreCubeList.Count - 1);

            if (spawnedCubesCount == 0)
            {
                ScoreCube firstScoreCube = Instantiate(_scoreCubeList[randomCubeIndex], transform.position, Quaternion.identity, transform);
                cubeTopPosition = firstScoreCube.GetCubeTopPosition();
                _scoreCubeList.RemoveAt(randomCubeIndex);
                spawnedCubesCount++;
                continue;
            }

            ScoreCube scoreCube = Instantiate(_scoreCubeList[randomCubeIndex], cubeTopPosition, Quaternion.identity, transform);
            cubeTopPosition = scoreCube.GetCubeTopPosition();
            _scoreCubeList.RemoveAt(randomCubeIndex);
            spawnedCubesCount++;
        }
    }
}
