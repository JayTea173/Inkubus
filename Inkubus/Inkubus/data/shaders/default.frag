#version 450 core
in vec2 vs_uv;
uniform ivec2 textureSize;

uniform sampler2D textureObject;
out vec4 color;

void main(void)
{
 color = texelFetch(textureObject, ivec2(vs_uv.x, vs_uv.y), 0);
// color = vec4(vs_uv.x, vs_uv.y, 0, 1);
 //vs_uv = vec2(vs_uv.x, vs_uv.y);
 //color = texture2D(textureObject, vs_uv + vec2(0.5/256, 0.5/256));
}