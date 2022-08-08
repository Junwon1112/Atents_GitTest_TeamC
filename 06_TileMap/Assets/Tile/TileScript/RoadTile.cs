using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RoadTile : Tile
{
    [System.Flags]
    enum AdjTilePosition : byte
    {
        None = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8,
        All = North | East | South | West
    }

    public Sprite[] sprites;
    public Sprite preview;

    // Ÿ���� �ٽ� �׷��� �� ȣ���
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">Ÿ�ϸʿ����� ��ġ</param>
    /// <param name="tilemap">�� Ÿ���� ������ Ÿ�ϸ�</param>
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        // �ڱ� �߽����� 9ĭ�� ���鼭 roadtile���� Ȯ���ϰ� roadtile�̸� refresh
        for(int yd = -1; yd <= 1; yd++)
        {
            for(int xd = -1; xd <= 1; xd++)
            {
                Vector3Int location = new(position.x + xd, position.y + yd, position.z);
                if(HasThisTile(tilemap, location))
                {
                    tilemap.RefreshTile(location);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">��ġ</param>
    /// <param name="tilemap">Ÿ�ϸ�</param>
    /// <param name="tileData">������ Ÿ�� ����</param>
    // Ÿ�Ͽ� ���� Ÿ�� ������ ������ ã�Ƽ� ����
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        //�ֺ� �����ġ�� roadtile�� �ִ��� ǥ���� ����ũ ����
        AdjTilePosition mask = AdjTilePosition.None;

        base.GetTileData(position, tilemap, ref tileData);
        mask |= HasThisTile(tilemap, position + new Vector3Int(0,1,0)) ? AdjTilePosition.North : 0;
        
        
        mask |= HasThisTile(tilemap, position + new Vector3Int(1, 0, 0)) ? AdjTilePosition.East : 0;
        mask |= HasThisTile(tilemap, position + new Vector3Int(0, -1, 0)) ? AdjTilePosition.South : 0;
        mask |= HasThisTile(tilemap, position + new Vector3Int(-1, 0, 0)) ? AdjTilePosition.West : 0;

        int index = GetIndex(mask);   
        if(index >= 0 && index <sprites.Length)
        {
            tileData.sprite = sprites[index];
            tileData.color = Color.white;
            var m = tileData.transform;
            m.SetTRS(Vector3.zero, GetRotation(mask), Vector3.one);
            tileData.transform = m;
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = ColliderType.None;
        }
        else
        {
            Debug.Log("����, ��������Ʈ�� ����");
        }
    
    }

    int GetIndex(AdjTilePosition mask)
    {
        switch (mask)
        {
            case AdjTilePosition.None:
                //�ֺ��� roadtile�� ����
                return 0;   //�ǹ� ����(�Ѱ��� ������ ���� �̹���. �����ϸ� 1��)
            case AdjTilePosition.North | AdjTilePosition.East:
            case AdjTilePosition.East | AdjTilePosition.South:
            case AdjTilePosition.North | AdjTilePosition.West:
            case AdjTilePosition.South | AdjTilePosition.West:
                //RoadTile 2���� ������ �ִµ� ���̴� ���
                return 1;   //�� �� ��� ��������Ʈ
            case AdjTilePosition.North:
            case AdjTilePosition.East:
            case AdjTilePosition.South:
            case AdjTilePosition.West:
            case AdjTilePosition.North | AdjTilePosition.South:
            case AdjTilePosition.East | AdjTilePosition.West:
                // RoadTile 1~2���� ������ �ִµ� 1�ڷ� �þ ���
                return 2;   // | �� ��� ��������Ʈ
            case AdjTilePosition.All & ~AdjTilePosition.North:
            case AdjTilePosition.All & ~AdjTilePosition.East:
            case AdjTilePosition.All & ~AdjTilePosition.South:
            case AdjTilePosition.All & ~AdjTilePosition.West:
                //roadTile 3���� ������ �ִ°��
                return 3;   // �� �� ��������Ʈ
            case AdjTilePosition.All:
                return 4;
        }
        return -1;
    }

        Quaternion GetRotation(AdjTilePosition mask)
    {
        switch (mask)
        {
            case AdjTilePosition.North | AdjTilePosition.West:
            case AdjTilePosition.East:
            case AdjTilePosition.West:
            case AdjTilePosition.East | AdjTilePosition.West:
            case AdjTilePosition.All & ~AdjTilePosition.West:
                return Quaternion.Euler(0, 0,-90);
            case AdjTilePosition.North | AdjTilePosition.East:
            case AdjTilePosition.All & ~AdjTilePosition.North:
                return Quaternion.Euler(0, 0, -180);
            case AdjTilePosition.East | AdjTilePosition.South:
            case AdjTilePosition.All & ~AdjTilePosition.East:
                return Quaternion.Euler(0, 0, -270);
        }
        return Quaternion.identity;
    }


    bool HasThisTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }


#if UNITY_EDITOR
    // RoadTile ������ ���� ����� ���� ���� �Լ�
    [MenuItem("Assets/Create/2D/Tiles/RoadTile")]
    public static void CreateRoadTile()
    {
        //������ ���� ��̸��� Ȯ���ڷ� ������ ����
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Road Tile",   //���� 
            "New Road Tile",    //�⺻ �̸�
            "Asset",           //Ȯ����
            "Save Road Tile",   //��� �޼���
            "Assets");          //���� �⺻ ����
        if(path == "")
        {
            return;
        }
        //������ ���� ��θ� ������� RoadTile ������ �ϳ� ����
        AssetDatabase.CreateAsset(CreateInstance<RoadTile>(), path);
    }
#endif

    
}