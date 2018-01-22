#version 450 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 uv;

out vec2 vs_uv;

layout (location = 20) uniform mat4 projection;
layout (location = 21) uniform mat4 modelView;
layout (location = 22) uniform vec2 textureSize;
layout (location = 23) uniform int frameCol;
layout (location = 24) uniform int frameRow;

void main(void)
{
	vs_uv = vec2(uv.x * textureSize.x + textureSize.x * frameCol, uv.y * textureSize.y + textureSize.y * frameRow);
	//vs_uv = vec2(uv.x * textureSize.x, uv.y * textureSize.y);
	gl_Position = projection * modelView * position;
}