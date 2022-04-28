using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;
    
    [SerializeField]
    Vector3 _cameraDirection = new Vector3(0.0f, 9.0f, -7.5f);
    
    [SerializeField] 
    GameObject _player = null;

    private Vector3 _delta;
    private Vector3 cameraPos;
    private float zoomSpeed = 0.1f;
    private int zoomSwitch = 0;

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
        //initialize the camera position to zoom out
        transform.position = _player.transform.position + _cameraDirection;
        _delta = transform.position - _player.transform.position;   //_delta 초기값 지정

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

            transform.position = _player.transform.position + _delta;   //플레이어 위치에 프레임마다 CameraZoom함수에서 갱신되는 _delta값을 더함.
                                                                        //이렇게 해야 줌 이동을 할 때 _delta값에 따라 부드럽게 카메라가 움직임. 이해 안가면 +_delta빼고 실행해 보자.
            transform.LookAt(_player.transform.position + Vector3.up);  //카메라 위치가 변하더라도 보는 위치는 고정

            float Distance = _delta.magnitude;

            Vector3 DirToCam = (transform.position - _player.transform.position).normalized;

            HitRayTransparentObject(_player.transform.position, DirToCam, Distance);    //플레이어 몸에서 카메라 방향으로 걸리는 장애물 반투명화
        }

        CameraZoom();
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

    void CameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
            zoomSwitch = 1;

        else if (scroll < 0)
            zoomSwitch = -1;

        if (zoomSwitch > 0)  //줌 인
        {
            cameraPos = _player.transform.position + Vector3.up + 2 * Vector3.back;
            transform.position = Vector3.Lerp(transform.position, cameraPos, zoomSpeed);

            _delta = transform.position - _player.transform.position;   //카메라와 플레이어 사이 거리 갱신
        }

        else if (zoomSwitch < 0) //줌 아웃
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position + _cameraDirection, zoomSpeed);

            _delta = transform.position - _player.transform.position;
        }
    }
    
    public void SetQaurterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
