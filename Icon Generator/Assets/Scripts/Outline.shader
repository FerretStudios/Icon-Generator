Shader "Unlit/Outline"
{
    Properties
    {
        _OutlineColor("Outline Colour", Color) = (1,1,1,1)
        _Thickness("Thickness", Range(1.00,5.0)) = 1.08
    }

    CGINCLUDE

    #include "UnityCG.cginc"


    struct appdata {

        float4 vertex : POSITION;
        float3 normal : NORMAL;

    };

    struct v2f{

        float4 pos : POSITION; 
        float4 color : COLOR;
        float3 normal : NORMAL;

    };

    float _Thickness;
    float4 _OutlineColor;

    v2f vert(appdata v){

        v.vertex.xyz *= _Thickness; //_Outline

        v2f o;

        o.pos = UnityObjectToClipPos(v.vertex);
        o.color = _OutlineColor;

        return o;
        
    };

    ENDCG

    SubShader
    {
        
        Pass //render outline
        {

            // Zwrite Off
            Cull Front

            Lighting Off

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            half4 frag(v2f i) : COLOR {

                return i.color;
            }

            ENDCG

        }

        // pass //normal render
        // {

            //     ZWrite On

            //     Material
            //     {

                //         Diffuse[_Color]
                //         Ambient[_Color]

            //     }

            //     Lighting On

            //     SetTexture[_MainTex]
            //     {

                //         ConstantColor[_Color]

            //     }

            //     SetTexture[_MainTex]{

                //         Combine previous * primary DOUBLE

            //     }

        // }
        
    }
}
