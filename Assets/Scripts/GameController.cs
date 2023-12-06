using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public const int columns = 2; // to identify number of columns
    public const int rows = 5; // to identify number of rows

    // to identify space between each card
    public const float Xspace = 3f;
    public const float Yspace = -3f;

    [SerializeField] private MainImage startObject;
    [SerializeField] private Sprite[] images;

    // randomize the arrangement 
    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for(int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    // from the start, a random arrangement will be given
    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4 };
        locations = Randomiser(locations);

        // getting position of the first object 
        Vector3 startPosition = startObject.transform.position;

        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                MainImage mainImage;
                if(i == 0 && j == 0)
                {
                    mainImage = startObject;
                }
                else
                {
                    mainImage = Instantiate(startObject) as MainImage;
                }
                int index = j * columns + i;
                int id = locations[index];
                mainImage.ChangeSprite(id, images[id]);

                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * i) + startPosition.y;

                mainImage.transform.position = new Vector3(positionX, positionY,startPosition.z);
            }
        }
    }


}
