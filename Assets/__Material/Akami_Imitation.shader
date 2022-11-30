Shader "Unlit/Akami_Imitation"
{
    Properties
    {
            // Setting input parameters
       _MainTex("Texture", 2D) = "white" {} //Main object texture
       _NoiseMap("Noise Map", 2D) = "white" {} //Noise map for outline 
       _NormalMap("Normal Map", 2D) = "white" {} //Normal map
       _Colour("Edge Colour", Color) = (1,1,1,1) //Shader override colour 
       _angleLimit("Angle Limit", Range(0,1)) = 0.5 //View angle Overide adjustment between 0 (right angle to view) and 1 (facing view)

    }
        SubShader
       {
           Tags { "RenderType" = "Opaque" }
           LOD 100

           Pass
           {
               CGPROGRAM
               #pragma vertex vert
               #pragma fragment frag
               // make fog work
              // #pragma multi_compile_fog

               #include "UnityCG.cginc"

               struct meshData
               {
                   float4 vertex : POSITION;
                   float2 uv : TEXCOORD0;
                   float3 normals : NORMAL;
               };

               struct interpolators
               {
                   float2 uv : TEXCOORD0; 
                   float4 vertex : SV_POSITION;
                   float3 normal : TEXCOORD1;                   
                   float3 viewDir : TEXCOORD2;
                   float2 MainUV : TEXCOORD3;
                   float2 NormalUV : TEXCOORD4;
                   //float viewAngl : TEXCOORD1;
               };

               sampler2D _MainTex;
               float4 _MainTex_ST;
               sampler2D _NoiseMap;
               sampler2D _NormalMap;
               float4 _NormalMap_ST;
               float4 _Colour;
               float _angleLimit;

               interpolators vert(meshData v)
               {
                   interpolators o;
                   o.vertex = UnityObjectToClipPos(v.vertex);
                   o.viewDir = normalize(WorldSpaceViewDir(v.vertex)); //noralised vector for vertex to view
                   o.normal = UnityObjectToWorldNormal(v.normals); //convert mesh normals to world space
                   //Apply transforms from  parameters to coordinates for texture mapping
                   o.MainUV = TRANSFORM_TEX(v.uv, _MainTex); 
                   o.NormalUV = TRANSFORM_TEX(v.uv, _NormalMap);

                   //Untransformed coordinates
                   o.uv = v.uv;
                   return o;
               }

               fixed4 frag(interpolators i) : SV_Target
               {
                   float3 nMap = 2 * tex2D(_NormalMap, i.NormalUV).xyz - 1;  //get vector from normal map
                   float normalMapShad =  dot(float3(0,0,1), nMap); //Compare normal map vector to perpendicular vector applies shading to angles from normal map
                   float viewAngl = dot(i.viewDir, i.normal);  //Returns a value between 0 and 1 that represents the angle between the mesh surface and the view direction


                   // in high angles returns to edge colour multiplied with noise applied otherwise return maintexture with normal map shading applied
                   float4 col = viewAngl > _angleLimit ? tex2D(_MainTex, i.MainUV) * normalMapShad : _Colour * tex2D(_NoiseMap, i.uv);
                   return col;
               }
               ENDCG
           }
       }
}
