using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;
    
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
    
    [SerializeField] 
    GameObject _player = null;
    /*
    [SerializeField]
    float _zoomSpeed = 0f;

    [SerializeField]
    float _zoomMax = 3.0f;

    [SerializeField]
    float _zoomMin = -2.0f;
    */
    public struct St_ObstacleRendererInfo
    {
        public int InstanceId;
        public MeshRenderer Mesh_Renderer;
        public Shader OrinShader;
    }

    private Dictionary<int, St_ObstacleRendererInfo> Dic_SavedObstaclesRendererInfo = new Dictionary<int, St_ObstacleRendererInfo>();
    private List<St_ObstacleRendererInfo> Lst_TransparentedRenderer = new List<St_ObstacleRendererInfo>();
    private Color ColorTransparent = new Color(1f, 1f, 1f, 0.2f);
    //private Color ColorOrin = new Color(1f, 1f, 1f, 1f);
    private string ShaderColorParamName = "_Color";
    private Shader TransparentShader;
    private RaycastHit[] TransparentHits;
    private LayerMask TransparentRayLayer;

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        TransparentRayLayer = 1 << LayerMask.NameToLayer("Block");
        TransparentShader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            if(_player.IsValid() == false)
            {
                return;
            }

            //반투명화된 오브젝트 원래 상태로 복귀
            if (Lst_TransparentedRenderer.Count > 0)
            {
                for (int i = 0; i < Lst_TransparentedRenderer.Count; i++)
                {
                    Lst_TransparentedRenderer[i].Mesh_Renderer.material.shader = Lst_TransparentedRenderer[i].OrinShader;
                }

                Lst_TransparentedRenderer.Clear();
            }

            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);

            float Distance = _delta.magnitude;

            Vector3 DirToCam = (transform.position - _player.transform.position).normalized;

            HitRayTransparentObject(_player.transform.position, DirToCam, Distance);    //플레이어 몸에서 카메라 방향으로 걸리는 장애물 반투명화
        }

        //CameraZoom();
    }

    void HitRayTransparentObject(Vector3 start, Vector3 direction, float distance)
    {
        TransparentHits = Physics.RaycastAll(start, direction, distance, TransparentRayLayer);

        for (int i = 0; i < TransparentHits.Length; i++)
        {
            int instanceid = TransparentHits[i].collider.GetInstanceID();

            //레이에 걸린 장애물이 컬렉션에 없으면 저장하기
            if (!Dic_SavedObstaclesRendererInfo.ContainsKey(instanceid))
            {
                MeshRenderer obsRenderer = TransparentHits[i].collider.gameObject.GetComponent<MeshRenderer>();
                St_ObstacleRendererInfo rendererInfo = new St_ObstacleRendererInfo();
                rendererInfo.InstanceId = instanceid; // 고유 인스턴스아이디
                rendererInfo.Mesh_Renderer = obsRenderer; // 메시렌더러
                rendererInfo.OrinShader = obsRenderer.material.shader; // 장애물의쉐이더

                Dic_SavedObstaclesRendererInfo[instanceid] = rendererInfo;
            }

            // 쉐이더 반투명으로 변경
            Dic_SavedObstaclesRendererInfo[instanceid].Mesh_Renderer.material.shader = TransparentShader;
            //알파값 줄인 쉐이더 색 변경
            Dic_SavedObstaclesRendererInfo[instanceid].Mesh_Renderer.material.SetColor(ShaderColorParamName, ColorTransparent);

            Lst_TransparentedRenderer.Add(Dic_SavedObstaclesRendererInfo[instanceid]);
        }
    }
    /*
    void CameraZoom()
    {
        float zoomDirection = Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y <= _zoomMax && zoomDirection > 0)
            return;

        if (transform.position.y >= _zoomMin && zoomDirection < 0)
            return;

        transform.position += transform.forward * zoomDirection * _zoomSpeed;
    }
    */
    public void SetQaurterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
