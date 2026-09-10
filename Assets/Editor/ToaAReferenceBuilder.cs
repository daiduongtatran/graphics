using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public static class ToaAReferenceBuilder
{
    private const string RootName = "ToaA_ChiTiet_Ref";
    private const string ScenePath = "Assets/Scenes/SampleScene.unity";

    [MenuItem("Tools/Toa A/Xoa sach va dung lai theo anh")]
    public static void Build()
    {
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        RemovePreviousTower();

        var white = AssetDatabase.LoadAssetAtPath<Material>("Assets/Mat_Trang.mat");
        var black = AssetDatabase.LoadAssetAtPath<Material>("Assets/Mat_Den.mat");
        var blue = AssetDatabase.LoadAssetAtPath<Material>("Assets/Mat__LamXanhLam.mat");
        var stone = EnsureMaterial("Assets/ToaA_DaXam.mat", new Color(.29f, .3f, .29f));
        var glass = EnsureMaterial("Assets/ToaA_KinhXam.mat", new Color(.055f, .09f, .105f));
        var green = EnsureMaterial("Assets/ToaA_CayXanh.mat", new Color(.16f, .42f, .18f));
        var grass = EnsureMaterial("Assets/ToaA_Co.mat", new Color(.3f, .58f, .2f));
        var paving = EnsureMaterial("Assets/ToaA_SanXam.mat", new Color(.34f, .38f, .4f));
        var trunk = EnsureMaterial("Assets/ToaA_ThanCay.mat", new Color(.24f, .12f, .06f));
        var root = new GameObject(RootName);

        AddCube(root.transform, "KhoiDeDaXam", new Vector3(-10, 4.1f, 5), new Vector3(12, 8.2f, 16), stone);
        AddCube(root.transform, "ThanMoi", new Vector3(-10, 20, 5), new Vector3(11.2f, 24, 15.2f), white);
        AddCube(root.transform, "MaiMoi", new Vector3(-10, 32.25f, 5), new Vector3(11.6f, .5f, 15.6f), white);
        AddCube(root.transform, "KinhTangTret", new Vector3(-10, 5.2f, -3.08f), new Vector3(10.8f, 5.4f, .12f), glass);
        AddCube(root.transform, "KinhTangTret_Sau", new Vector3(-10, 5.2f, 13.08f), new Vector3(10.8f, 5.4f, .12f), glass);
        AddCube(root.transform, "LoiVao", new Vector3(-10, 3.2f, -3.3f), new Vector3(3.6f, 4.8f, .16f), glass);

        for (var bay = 0; bay < 5; bay++)
        {
            var x = -14.25f + bay * 2.15f;
            AddCube(root.transform, "KhungDe_Cot_" + bay, new Vector3(x, 4.4f, -3.38f), new Vector3(.18f, 6.8f, .22f), stone);
        }
        AddCube(root.transform, "MaiHienSanh", new Vector3(-10, 7.65f, -4.25f), new Vector3(10.8f, .18f, 2.2f), glass);
        AddCube(root.transform, "GheDa_De", new Vector3(-10, .55f, -3.62f), new Vector3(11.8f, .5f, .55f), stone);
        for (var bay = 0; bay < 5; bay++)
        {
            var x = -14.25f + bay * 2.15f;
            AddCube(root.transform, "KinhDe_O" + bay, new Vector3(x, 4.35f, -3.5f), new Vector3(1.75f, 5.2f, .08f), glass);
            AddCube(root.transform, "KinhDe_NgangTren_" + bay, new Vector3(x, 6.85f, -3.62f), new Vector3(1.9f, .1f, .12f), stone);
            AddCube(root.transform, "KinhDe_NgangDuoi_" + bay, new Vector3(x, 1.85f, -3.62f), new Vector3(1.9f, .1f, .12f), stone);
            for (var mullion = 0; mullion < 4; mullion++)
            {
                AddCube(root.transform, "KinhDe_Mullion_" + bay + "_" + mullion, new Vector3(x - .68f + mullion * .45f, 4.35f, -3.62f), new Vector3(.035f, 5.05f, .06f), black);
            }
        }
        for (var brace = 0; brace < 5; brace++)
        {
            var x = -14.2f + brace * 2.1f;
            var support = AddCube(root.transform, "MaiHien_Treo_" + brace, new Vector3(x, 7.2f, -4.95f), new Vector3(.08f, 1.15f, .08f), stone);
            support.transform.rotation = Quaternion.Euler(25f, 0, 0);
        }

        for (var floor = 0; floor < 12; floor++)
        {
            var y = 9f + floor * 2f;
            AddCube(root.transform, "KinhTruoc_Trai_" + floor, new Vector3(-12.85f, y + .85f, -2.76f), new Vector3(5.05f, .72f, .08f), glass);
            AddCube(root.transform, "KinhTruoc_Phai_" + floor, new Vector3(-7.15f, y + .85f, -2.76f), new Vector3(5.05f, .72f, .08f), glass);
            AddCube(root.transform, "KinhSau_" + floor, new Vector3(-10, y + .85f, 12.76f), new Vector3(10.7f, .72f, .08f), glass);
            AddCube(root.transform, "KinhTrai_" + floor, new Vector3(-15.66f, y + .85f, 5f), new Vector3(.08f, .72f, 14.8f), glass);
            AddCube(root.transform, "KinhPhai_" + floor, new Vector3(-4.34f, y + .85f, 5f), new Vector3(.08f, .72f, 14.8f), glass);
        }

        AddCube(root.transform, "LoiKinhTrungTam", new Vector3(-10, 20.3f, -2.82f), new Vector3(2.35f, 23.6f, .1f), glass);
        for (var mullion = 0; mullion < 7; mullion++)
        {
            AddCube(root.transform, "LoiKinh_Tru_" + mullion, new Vector3(-11f + mullion * .33f, 20.3f, -2.94f), new Vector3(.045f, 23.4f, .08f), blue);
        }
        for (var floor = 0; floor < 12; floor++)
        {
            var y = 8.55f + floor * 2f;
            AddCube(root.transform, "GioiHanTang_Truoc_" + floor, new Vector3(-10, y, -2.98f), new Vector3(11.35f, .1f, .12f), white);
            AddCube(root.transform, "ThongGio_Trai_" + floor, new Vector3(-13.05f, y + .8f, -2.98f), new Vector3(.55f, .28f, .06f), black);
            AddCube(root.transform, "ThongGio_Phai_" + floor, new Vector3(-6.95f, y + .8f, -2.98f), new Vector3(.55f, .28f, .06f), black);
        }
        AddPodiumSide(root.transform, "DeTrai", -16.06f, 5f, glass, stone, black);
        AddPodiumSide(root.transform, "DePhai", -3.94f, 5f, glass, stone, black);
        AddPodiumBack(root.transform, "DeSau", 13.06f, glass, stone, black);

        for (var floor = 0; floor < 12; floor++)
        {
            var y = 8.55f + floor * 2f;
            AddCube(root.transform, "GioiHanTang_Trai_" + floor, new Vector3(-15.78f, y, 5f), new Vector3(.12f, .1f, 15.8f), white);
            AddCube(root.transform, "GioiHanTang_Phai_" + floor, new Vector3(-4.22f, y, 5f), new Vector3(.12f, .1f, 15.8f), white);
        }

        AddCube(root.transform, "DaiSan_MatTruoc", new Vector3(-10, 8.45f, -2.68f), new Vector3(11.4f, .28f, .38f), white);
        AddCube(root.transform, "DaiSan_MatSau", new Vector3(-10, 8.45f, 12.68f), new Vector3(11.4f, .28f, .38f), white);
        for (var floor = 1; floor <= 11; floor++)
        {
            var y = 8.45f + floor * 2f;
            AddCube(root.transform, "DaiSan_Truoc_" + floor, new Vector3(-10, y, -2.68f), new Vector3(11.4f, .22f, .38f), white);
            AddCube(root.transform, "DaiSan_Sau_" + floor, new Vector3(-10, y, 12.68f), new Vector3(11.4f, .22f, .38f), white);
            AddCube(root.transform, "DaiSan_Trai_" + floor, new Vector3(-15.68f, y, 5f), new Vector3(.38f, .22f, 15.2f), white);
            AddCube(root.transform, "DaiSan_Phai_" + floor, new Vector3(-4.32f, y, 5f), new Vector3(.38f, .22f, 15.2f), white);
        }

        var finXs = new[] { -11.65f, -11.15f, -10.65f, -10.15f, -9.65f, -9.15f, -8.65f };
        for (var i = 0; i < finXs.Length; i++)
        {
            AddCube(root.transform, "LamTrungTam_Truoc_" + i, new Vector3(finXs[i], 20.5f, -2.9f), new Vector3(.3f, 23.5f, .16f), blue);
            AddCube(root.transform, "LamTrungTam_Sau_" + i, new Vector3(finXs[i], 20.5f, 12.9f), new Vector3(.3f, 23.5f, .16f), blue);
        }
        var sideFinZs = new[] { 3.35f, 3.85f, 4.35f, 4.85f, 5.35f, 5.85f, 6.35f };
        for (var i = 0; i < sideFinZs.Length; i++)
        {
            AddCube(root.transform, "LamTrungTam_Trai_" + i, new Vector3(-15.9f, 20.5f, sideFinZs[i]), new Vector3(.16f, 23.5f, .3f), blue);
            AddCube(root.transform, "LamTrungTam_Phai_" + i, new Vector3(-4.1f, 20.5f, sideFinZs[i]), new Vector3(.16f, 23.5f, .3f), blue);
        }

        AddRoofWall(root.transform, "MaiTruoc", false, -2.72f, white);
        AddRoofWall(root.transform, "MaiSau", false, 12.72f, white);
        AddRoofWall(root.transform, "MaiTrai", true, -15.72f, white);
        AddRoofWall(root.transform, "MaiPhai", true, -4.28f, white);

        AddCube(root.transform, "SanDe_MatTruoc", new Vector3(-10, .35f, -3.25f), new Vector3(13.2f, .7f, .45f), white);
        AddCube(root.transform, "SanDe_MatSau", new Vector3(-10, .35f, 13.25f), new Vector3(13.2f, .7f, .45f), white);

        AddCube(root.transform, "SanTruoc_LatDa", new Vector3(-10, .08f, -9.2f), new Vector3(18f, .16f, 11.2f), paving);
        AddCube(root.transform, "ThamCo_Trai", new Vector3(-16.2f, .18f, -9.6f), new Vector3(3.4f, .28f, 9.2f), grass);
        AddCube(root.transform, "ThamCo_Phai", new Vector3(-3.8f, .18f, -9.6f), new Vector3(3.4f, .28f, 9.2f), grass);
        AddCube(root.transform, "LoiDiVao", new Vector3(-10, .2f, -8.3f), new Vector3(5.2f, .22f, 8.6f), white);

        AddCube(root.transform, "SanKinh_Sanh", new Vector3(-10, 4.2f, -3.38f), new Vector3(9.8f, 5.8f, .14f), black);
        AddCube(root.transform, "KhungSanh_Tren", new Vector3(-10, 7.1f, -3.52f), new Vector3(10.4f, .28f, .34f), white);
        AddCube(root.transform, "KhungSanh_Duoi", new Vector3(-10, 1.35f, -3.52f), new Vector3(10.4f, .28f, .34f), white);
        for (var column = 0; column < 6; column++)
        {
            AddCube(root.transform, "KhungSanh_Cot_" + column, new Vector3(-14.3f + column * 1.72f, 4.2f, -3.52f), new Vector3(.16f, 5.8f, .3f), white);
        }

        AddCube(root.transform, "MaiHien_MatTien", new Vector3(-10, 7.65f, -5.05f), new Vector3(11.6f, .3f, 3.2f), white);
        AddCube(root.transform, "MaiHien_VienDen", new Vector3(-10, 7.45f, -6.62f), new Vector3(11.8f, .18f, .18f), black);
        for (var column = 0; column < 4; column++)
        {
            AddCube(root.transform, "MaiHien_Cot_" + column, new Vector3(-14.25f + column * 2.83f, 3.75f, -6.15f), new Vector3(.3f, 7.2f, .3f), white);
        }

        for (var step = 0; step < 3; step++)
        {
            AddCube(root.transform, "BacSanh_" + step, new Vector3(-10, .35f + step * .24f, -4.1f - step * .62f), new Vector3(8.4f - step * .7f, .22f, 1.1f), white);
        }

        for (var tree = 0; tree < 9; tree++)
        {
            var x = -18f + tree * 2f;
            AddImportedTree(root.transform, "CayMatTien_" + tree, new Vector3(x, .2f, -14.2f), tree % 4);
        }
        AddFrontageBoundary(root.transform, "MatTienTruong", 0f, white, black, blue, grass, paving, trunk, green);

        RotateTowerOnly(root.transform);
        Selection.activeGameObject = root;
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();
    }

    private static void RemovePreviousTower()
    {
        var objects = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include);
        var objectsToDestroy = new List<GameObject>();
        foreach (var gameObject in objects)
        {
            if (gameObject == null || !IsLegacyTowerObject(gameObject.name))
            {
                continue;
            }

            objectsToDestroy.Add(gameObject);
        }

        objectsToDestroy.Sort((first, second) => GetDepth(second.transform).CompareTo(GetDepth(first.transform)));
        foreach (var gameObject in objectsToDestroy)
        {
            if (gameObject != null)
            {
                Object.DestroyImmediate(gameObject);
            }
        }
    }

    private static int GetDepth(Transform transform)
    {
        var depth = 0;
        while (transform.parent != null)
        {
            depth++;
            transform = transform.parent;
        }
        return depth;
    }

    private static void RotateTowerOnly(Transform root)
    {
        var pivot = new Vector3(-10f, 0f, 5f);
        var rotation = Quaternion.Euler(0f, -90f, 0f);
        foreach (Transform child in root)
        {
            if (IsLandscapeObject(child.name))
            {
                continue;
            }

            child.position = pivot + rotation * (child.position - pivot);
            child.rotation = rotation * child.rotation;
        }
    }

    private static bool IsLandscapeObject(string objectName)
    {
        return objectName.StartsWith("MatTien") || objectName.StartsWith("Cay") || objectName.StartsWith("BuiCay") ||
            objectName.StartsWith("ThamCo") || objectName.StartsWith("SanTruoc") || objectName.StartsWith("LoiDi") ||
            objectName.StartsWith("Bo") || objectName.StartsWith("Cong");
    }

    private static bool IsLegacyTowerObject(string objectName)
    {
        return objectName.StartsWith("ToaA_") || objectName.StartsWith("Kinh_") || objectName.StartsWith("Lam_") ||
            objectName.StartsWith("HeThongLam") || objectName.StartsWith("TuongMai") || objectName.StartsWith("Thanh_") ||
            objectName.StartsWith("Cot_") || objectName.StartsWith("CuaKinh") || objectName.StartsWith("NenLam_") ||
            objectName.StartsWith("Cong") || objectName.StartsWith("De") || objectName.StartsWith("KhoiDe") ||
            objectName.StartsWith("ThanMoi") || objectName.StartsWith("MaiMoi") || objectName.StartsWith("KinhTang") ||
            objectName.StartsWith("LoiVao") || objectName.StartsWith("DaiSan") || objectName.StartsWith("LamTrungTam") ||
            objectName.StartsWith("KhungMai") || objectName.StartsWith("CotParapet") || objectName.StartsWith("ThanhDinh") ||
            objectName.StartsWith("SanDe") || objectName.StartsWith("SanTruoc") || objectName.StartsWith("ThamCo") ||
            objectName.StartsWith("LoiDi") || objectName.StartsWith("SanKinh") || objectName.StartsWith("KhungSanh") ||
            objectName.StartsWith("MaiHien") || objectName.StartsWith("BacSanh") || objectName.StartsWith("Cay") ||
            objectName.StartsWith("BuiCay") || objectName.StartsWith("GheDa") || objectName.StartsWith("GioiHan") ||
            objectName.StartsWith("ThongGio") || objectName.StartsWith("LoiKinh") || objectName.StartsWith("MaiKhung") ||
            objectName.StartsWith("MatTien") || objectName.StartsWith("Tree9") || objectName == RootName;
    }

    private static GameObject AddCube(Transform parent, string objectName, Vector3 position, Vector3 scale, Material material)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = objectName;
        cube.transform.SetParent(parent);
        cube.transform.position = position;
        cube.transform.localScale = scale;
        cube.GetComponent<MeshRenderer>().sharedMaterial = material;
        return cube;
    }

    private static GameObject AddSphere(Transform parent, string objectName, Vector3 position, Vector3 scale, Material material)
    {
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = objectName;
        sphere.transform.SetParent(parent);
        sphere.transform.position = position;
        sphere.transform.localScale = scale;
        sphere.GetComponent<MeshRenderer>().sharedMaterial = material;
        return sphere;
    }

    private static void AddTree(Transform parent, string objectName, Vector3 position, Material trunk, Material green)
    {
        AddCube(parent, objectName + "_Than", position + new Vector3(0, 1.35f, 0), new Vector3(.24f, 2.7f, .24f), trunk);
        AddSphere(parent, objectName + "_TanLaGiua", position + new Vector3(0, 2.35f, 0), new Vector3(1.3f, 1.1f, 1.05f), green);
        AddSphere(parent, objectName + "_TanLaTrai", position + new Vector3(-.7f, 2.75f, .05f), new Vector3(1.05f, .85f, .9f), green);
        AddSphere(parent, objectName + "_TanLaPhai", position + new Vector3(.65f, 2.9f, -.08f), new Vector3(1.15f, .95f, 1f), green);
        AddSphere(parent, objectName + "_TanLaTren", position + new Vector3(-.15f, 3.45f, .08f), new Vector3(.85f, .72f, .8f), green);
    }

    private static void AddImportedTree(Transform parent, string objectName, Vector3 position, int variant)
    {
        var prefabPath = "Assets/Tree9/Tree9_Leaf.fbx";
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefab == null)
        {
            return;
        }

        var tree = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
        tree.name = objectName;
        tree.transform.position = position;
        tree.transform.localScale = Vector3.one * (1.4f + variant * .12f);
        tree.transform.rotation = Quaternion.Euler(0f, (variant * 47f) % 360f, 0f);
    }

    private static void AddFrontageBoundary(Transform parent, string objectName, float centerX, Material white, Material black, Material blue, Material grass, Material paving, Material trunk, Material green)
    {
        const float frontageWidth = 40f;
        const float frontageZ = -19.25f;
        const float entranceCenter = 16f;
        const float entranceWidth = 8f;
        const float boundary = 19.25f;
        var leftEdge = centerX - frontageWidth / 2f;
        var rightEdge = centerX + frontageWidth / 2f;

        AddCube(parent, objectName + "_ViaHe", new Vector3(centerX, .16f, frontageZ), new Vector3(frontageWidth, .28f, 2.1f), white);
        AddCube(parent, objectName + "_DuongXeVao", new Vector3(entranceCenter, .08f, -16.35f), new Vector3(entranceWidth, .16f, 5.8f), paving);
        AddCube(parent, objectName + "_CoTrai", new Vector3((leftEdge + entranceCenter - entranceWidth / 2f) / 2f, .22f, -16.35f), new Vector3(entranceCenter - entranceWidth / 2f - leftEdge, .28f, 5.8f), grass);
        AddCube(parent, objectName + "_CoPhai", new Vector3((entranceCenter + entranceWidth / 2f + rightEdge) / 2f, .22f, -16.35f), new Vector3(rightEdge - entranceCenter - entranceWidth / 2f, .28f, 5.8f), grass);

        AddCube(parent, objectName + "_TuongTrai", new Vector3(leftEdge + 2.1f, 1.05f, frontageZ), new Vector3(4.2f, 1.8f, .72f), white);
        AddCube(parent, objectName + "_TuongPhai", new Vector3(rightEdge - 2.1f, 1.05f, frontageZ), new Vector3(4.2f, 1.8f, .72f), white);
        AddCube(parent, objectName + "_BoCongTrai", new Vector3((leftEdge + entranceCenter - entranceWidth / 2f) / 2f, .62f, frontageZ), new Vector3(entranceCenter - entranceWidth / 2f - leftEdge, .16f, .18f), white);
        AddCube(parent, objectName + "_BoCongPhai", new Vector3((entranceCenter + entranceWidth / 2f + rightEdge) / 2f, .62f, frontageZ), new Vector3(rightEdge - entranceCenter - entranceWidth / 2f, .16f, .18f), white);

        AddFenceRun(parent, objectName + "_Truoc_Trai", -boundary, entranceCenter - entranceWidth / 2f, frontageZ, false, white);
        AddFenceRun(parent, objectName + "_Truoc_Phai", entranceCenter + entranceWidth / 2f, boundary, frontageZ, false, white);
        AddPerimeterFence(parent, objectName + "_Trai", -boundary, -boundary, boundary, true, white);
        AddPerimeterFence(parent, objectName + "_Phai", boundary, -boundary, boundary, true, white);
        AddPerimeterFence(parent, objectName + "_Sau", -boundary, boundary, boundary, false, white);

        for (var tree = 0; tree < 16; tree++)
        {
            var x = leftEdge + 1.8f + tree * 2.4f;
            if (x > entranceCenter - entranceWidth / 2f - .8f && x < entranceCenter + entranceWidth / 2f + .8f)
            {
                continue;
            }
            AddImportedTree(parent, objectName + "_CayNgoai_" + tree, new Vector3(x, .2f, -15.1f), tree % 4);
        }
    }

    private static void AddPerimeterFence(Transform parent, string objectName, float fixedAxis, float start, float end, bool side, Material white)
    {
        AddFenceRun(parent, objectName, start, end, fixedAxis, side, white);
    }

    private static void AddFenceRun(Transform parent, string objectName, float start, float end, float fixedAxis, bool side, Material white)
    {
        const float spacing = 1.2f;
        const float baseHeight = .65f;
        const float fenceHeight = 1.65f;
        var count = Mathf.Max(1, Mathf.FloorToInt((end - start) / spacing));
        var midpoint = (start + end) / 2f;
        var length = end - start;
        var basePosition = side ? new Vector3(fixedAxis, baseHeight / 2f, midpoint) : new Vector3(midpoint, baseHeight / 2f, fixedAxis);
        var baseScale = side ? new Vector3(.55f, baseHeight, length) : new Vector3(length, baseHeight, .55f);
        AddCube(parent, objectName + "_ChanTuong", basePosition, baseScale, white);

        for (var index = 0; index <= count; index++)
        {
            var coordinate = index == count ? end : start + index * spacing;
            var position = side ? new Vector3(fixedAxis, 1.05f, coordinate) : new Vector3(coordinate, 1.05f, fixedAxis);
            AddCube(parent, objectName + "_CotLon_" + index, position, side ? new Vector3(.32f, 2.05f, .32f) : new Vector3(.32f, 2.05f, .32f), white);
            AddCube(parent, objectName + "_ThanhTren_" + index, side ? new Vector3(fixedAxis, 1.85f, coordinate) : new Vector3(coordinate, 1.85f, fixedAxis), side ? new Vector3(.34f, .14f, .34f) : new Vector3(.34f, .14f, .34f), white);
            AddCube(parent, objectName + "_ThanhDuoi_" + index, side ? new Vector3(fixedAxis, .85f, coordinate) : new Vector3(coordinate, .85f, fixedAxis), side ? new Vector3(.34f, .12f, .34f) : new Vector3(.34f, .12f, .34f), white);
            if (index < count)
            {
                var next = start + (index + 1) * spacing;
                if (next > end) next = end;
                var railLength = next - coordinate;
                var railPosition = side ? new Vector3(fixedAxis, 1.25f, coordinate + railLength / 2f) : new Vector3(coordinate + railLength / 2f, 1.25f, fixedAxis);
                var railScale = side ? new Vector3(.16f, .12f, railLength) : new Vector3(railLength, .12f, .16f);
                AddCube(parent, objectName + "_ThanhGiua_" + index, railPosition, railScale, white);
                var topPosition = side ? new Vector3(fixedAxis, 1.85f, coordinate + railLength / 2f) : new Vector3(coordinate + railLength / 2f, 1.85f, fixedAxis);
                var bottomPosition = side ? new Vector3(fixedAxis, .85f, coordinate + railLength / 2f) : new Vector3(coordinate + railLength / 2f, .85f, fixedAxis);
                var horizontalScale = side ? new Vector3(.18f, .14f, railLength) : new Vector3(railLength, .14f, .18f);
                AddCube(parent, objectName + "_ThanhTrenNoi_" + index, topPosition, horizontalScale, white);
                AddCube(parent, objectName + "_ThanhDuoiNoi_" + index, bottomPosition, horizontalScale, white);
            }
        }
    }

    private static void AddTextLabel(Transform parent, string objectName, string text, Vector3 position, Vector3 scale, Material material)
    {
        var label = new GameObject(objectName);
        label.transform.SetParent(parent);
        label.transform.position = position;
        label.transform.localScale = scale;
        var textMesh = label.AddComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = 48;
        textMesh.characterSize = 1f;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.color = material.color;
    }

    private static void AddPodiumSide(Transform parent, string objectName, float x, float centerZ, Material glass, Material stone, Material black)
    {
        AddCube(parent, objectName + "_Nen", new Vector3(x, 4.2f, centerZ), new Vector3(.08f, 5.6f, 14.1f), glass);
        for (var bay = 0; bay < 5; bay++)
        {
            var z = -1.1f + bay * 3.05f;
            AddCube(parent, objectName + "_Cot_" + bay, new Vector3(x - .12f, 4.25f, z), new Vector3(.22f, 6.6f, .2f), stone);
            for (var mullion = 0; mullion < 3; mullion++)
            {
                AddCube(parent, objectName + "_Mullion_" + bay + "_" + mullion, new Vector3(x - .13f, 4.25f, z - 1.05f + mullion * 1.05f), new Vector3(.05f, 5.2f, .05f), black);
            }
        }
        AddCube(parent, objectName + "_GioiHanTren", new Vector3(x - .14f, 7.15f, centerZ), new Vector3(.18f, .16f, 15f), stone);
        AddCube(parent, objectName + "_GioiHanDuoi", new Vector3(x - .14f, 1.35f, centerZ), new Vector3(.18f, .16f, 15f), stone);
    }

    private static void AddPodiumBack(Transform parent, string objectName, float z, Material glass, Material stone, Material black)
    {
        AddCube(parent, objectName + "_Nen", new Vector3(-10, 4.2f, z), new Vector3(11.5f, 5.6f, .08f), glass);
        for (var bay = 0; bay < 5; bay++)
        {
            var x = -14.2f + bay * 2.1f;
            AddCube(parent, objectName + "_Cot_" + bay, new Vector3(x, 4.25f, z + .12f), new Vector3(.2f, 6.6f, .22f), stone);
            for (var mullion = 0; mullion < 4; mullion++)
            {
                AddCube(parent, objectName + "_Mullion_" + bay + "_" + mullion, new Vector3(x - .7f + mullion * .46f, 4.25f, z + .13f), new Vector3(.04f, 5.2f, .05f), black);
            }
        }
    }

    private static void AddRoofWall(Transform parent, string objectName, bool sideWall, float fixedAxis, Material white)
    {
        const float baseY = 32.62f;
        const float wallHeight = 1.85f;
        AddCube(parent, objectName + "_Chan", sideWall ? new Vector3(fixedAxis, baseY, 5f) : new Vector3(-10, baseY, fixedAxis), sideWall ? new Vector3(.32f, .24f, 15.5f) : new Vector3(11.6f, .24f, .32f), white);
        AddCube(parent, objectName + "_Dinh", sideWall ? new Vector3(fixedAxis, baseY + wallHeight, 5f) : new Vector3(-10, baseY + wallHeight, fixedAxis), sideWall ? new Vector3(.32f, .24f, 15.5f) : new Vector3(11.6f, .24f, .32f), white);

        for (var post = 0; post < 5; post++)
        {
            var coordinate = sideWall ? -2.5f + post * 3.75f : -15.5f + post * 2.75f;
            var position = sideWall ? new Vector3(fixedAxis, baseY + wallHeight / 2f, coordinate) : new Vector3(coordinate, baseY + wallHeight / 2f, fixedAxis);
            var scale = sideWall ? new Vector3(.32f, wallHeight, .26f) : new Vector3(.26f, wallHeight, .32f);
            AddCube(parent, objectName + "_Tru_" + post, position, scale, white);
        }
    }

    private static Material EnsureMaterial(string path, Color color)
    {
        var material = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (material != null)
        {
            return material;
        }

        material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.color = color;
        AssetDatabase.CreateAsset(material, path);
        return material;
    }
}