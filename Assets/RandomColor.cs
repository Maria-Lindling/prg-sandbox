using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class RandomColor : MonoBehaviour
{
    [SerializeField] private Image m_image ;
    [SerializeField] private float changeInterval = 1.0f ;
    private float localChangesInterval;
    // Start is called before the first frame update
    void Start()
    {
        localChangesInterval = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        localChangesInterval += Time.deltaTime;
        if (localChangesInterval > changeInterval) return;
        localChangesInterval = 0.0f;
        m_image.color = new Color( Random.value, Random.value, Random.value ) ;
        m_image.rectTransform.localScale = new Vector3(Random.value,Random.value,Random.value);
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
