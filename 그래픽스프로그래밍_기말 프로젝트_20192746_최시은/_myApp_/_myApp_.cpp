#include <sb7.h>
#include <vmath.h>
#include <shader.h>
#include <vector>

#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"
#include "Model.h"

class my_application : public sb7::application
{
public:
	GLuint compile_shader(void) {
		GLuint vertex_shader = sb7::shader::load("multiple_lights_vs.glsl", GL_VERTEX_SHADER);
		GLuint fragment_shader = sb7::shader::load("multiple_lights_fs.glsl", GL_FRAGMENT_SHADER);

		// 프로그램을 생성하고 쉐이더를 Attach시키고 링크한다.
		GLuint program = glCreateProgram();
		glAttachShader(program, vertex_shader);
		glAttachShader(program, fragment_shader);
		glLinkProgram(program);

		// 이제 프로그램이 쉐이더를 소유하므로 쉐이더를 삭제한다.
		glDeleteShader(vertex_shader);
		glDeleteShader(fragment_shader);

		return program;
	}

	GLuint obj_shader(void){
		GLuint vertex_shader = sb7::shader::load("multiple_lights_vs.glsl", GL_VERTEX_SHADER);
		GLuint fragment_shader = sb7::shader::load("objColor_fs.glsl", GL_FRAGMENT_SHADER);

		// 프로그램을 생성하고 쉐이더를 Attach시키고 링크한다.
		GLuint program = glCreateProgram();
		glAttachShader(program, vertex_shader);
		glAttachShader(program, fragment_shader);
		glLinkProgram(program);

		// 이제 프로그램이 쉐이더를 소유하므로 쉐이더를 삭제한다.
		glDeleteShader(vertex_shader);
		glDeleteShader(fragment_shader);

		return program;
	}

	void createModel(Model *name, const char* obj_filename, const char* diffuse_filename) {
		name->init();
		name->loadOBJ(obj_filename);
		name->loadDiffuseMap(diffuse_filename);
	}

	virtual void startup() {
		initValues();

		shader_program = compile_shader();
		shader_program2 = obj_shader();

		stbi_set_flip_vertically_on_load(true);

		// 객체 정의  --------------------------------------------------
		createModel(&objModel, "shiba.obj", "shiba.png");			// 주인공
		createModel(&bodyGuard, "shiba.obj", "bodyGuard.png");		// 문지기
		createModel(&student, "shiba.obj", "studentShiba.png");		// 학생
		createModel(&door, "door.obj", "door.png");					// 문
		createModel(&exitSign, "exitSign.obj", "exitSign.png");		// exitSign
		createModel(&fence, "fence.obj", "fence.png");				// fence
		createModel(&feed, "feed.obj", "orange.png");					// feed
		createModel(&chest, "chest.obj", "chest.png");				// chest

		// 주인공 광원 객체 정의 --------------------------------------------------
		GLfloat light_vertices[] = {
			1.0f, 0.0f, -1.0f,    // 우측 상단
			-1.0f, 0.0f, -1.0f,   // 좌측 상단
			-1.0f, 0.0f, 1.0f,    // 좌측 하단
			1.0f, 0.0f, 1.0f,     // 우측 하단
			0.0f, 1.0f, 0.0f,     // 상단 꼭지점
			0.0f, -1.0f, 0.0f,    // 하단 꼭지점
		};

		GLuint light_indices[] = {
			4, 0, 1,
			4, 1, 2,
			4, 2, 3,
			4, 3, 0,

			5, 1, 0,
			5, 2, 1,
			5, 3, 2,
			5, 0, 3,
		};

		lightModel.init();
		lightModel.setupMesh(6, light_vertices);
		lightModel.setupIndices(24, light_indices);

		// 바닥 정의 ----------------------------------------------------------------
		floorModel.init();
		floorModel.loadDiffuseMap("floor.jpg");
		GLfloat floor_pos[] = {
			0.5f, 0.0f, -2.0f,
			-0.5f, 0.0f, -2.0f,
			-0.5f, 0.0f, 2.0f,
			0.5f, 0.0f, 2.0f
		};
		float floor_s = 3.0f, floor_t = 6.0f;
		GLfloat floor_tex[] = {
			 floor_s, floor_t,
			 0.0f, floor_t,
			 0.0f, 0.0f,
			floor_s, 0.0f
		};
		GLfloat floor_nor[] = {
			 0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			 0.0f, 1.0f, 1.0f,
			0.0f, 1.0f, 0.0f
		};
		GLuint floor_indices[] = {
			0, 1, 2,
			0, 2, 3	
		};
		floorModel.setupMesh(4, floor_pos, floor_tex, floor_nor);
		floorModel.setupIndices(6, floor_indices);

		// 왼쪽 벽 정의 ----------------------------------------------------------------
		leftWallModel.init();
		leftWallModel.loadDiffuseMap("whiteWall.jpg");
		GLfloat leftWall_pos[] = {
			-0.5f, 0.0f, -2.0f,
			-0.5f, 1.0f, -2.0f,
			-0.5f, 1.0f, 2.0f,
			-0.5f, 0.0f, 2.0f
		};
		float leftWall_s = 3.0f, leftWall_t = 3.0f;
		GLfloat leftWall_tex[] = {
			leftWall_s, 0.0f,
			 leftWall_s, leftWall_t,
			 0.0f, leftWall_t,
			 0.0f, 0.0f
		};
		GLfloat leftWall_nor[] = {
			0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f
		};
		GLuint leftWall_indices[] = {
			0, 1, 2,
			0, 2, 3	
		};
		leftWallModel.setupMesh(4, leftWall_pos, leftWall_tex, leftWall_nor);
		leftWallModel.setupIndices(6, leftWall_indices);

		// 오른쪽 벽 정의 ----------------------------------------------------------------
		rightWallModel.init();
		rightWallModel.loadDiffuseMap("whiteWall.jpg");
		GLfloat rightWall_pos[] = {
			0.5f, 0.0f, -2.0f, // 0 - 좌하
			0.5f, 1.0f, -2.0f, // 1 - 좌상
			0.5f, 1.0f, 2.0f,  // 2 - 우상
			0.5f, 0.0f, 2.0f   // 3 - 우하
		};
		float rightWall_s = 3.0f, rightWall_t = 3.0f;
		GLfloat rightWall_tex[] = {
			0.0f, 0.0f,
			0.0f, rightWall_t,
			rightWall_s, rightWall_t,
			rightWall_s, 0.0f

		};
		GLfloat rightWall_nor[] = {
			0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f
		};
		GLuint rightWall_indices[] = {
			2, 1, 0,
			2, 0, 3	
		};
		rightWallModel.setupMesh(4, rightWall_pos, rightWall_tex, rightWall_nor);
		rightWallModel.setupIndices(6, rightWall_indices);

		// 앞 벽 정의 ------------------------------------------------------------
		frontWallModel.init();
		frontWallModel.loadDiffuseMap("whiteWall.jpg");
		GLfloat frontWall_pos[] = {
			-0.5f, 0.0f, -1.0f, // 0 - 좌하
			-0.5f, 2.0f, -1.0f, // 1 - 좌상
			0.5f, 2.0f, -1.0f,  // 2 - 우상
			0.5f, 0.0f, -1.0f   // 3 - 우하
		};
		float frontWall_s = 3.0f, frontWall_t = 3.0f;
		GLfloat frontWall_tex[] = {
			0.0f, 0.0f,
			0.0f, frontWall_t,
			 frontWall_s, frontWall_t,
			 frontWall_s, 0.0f

		};
		GLfloat frontWall_nor[] = {
			 0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			 0.0f, 1.0f, 0.0f,
			0.0f, 1.0f, 0.0f
		};
		GLuint frontWall_indices[] = {
			2, 1, 0,	
			2, 0, 3		
		};
		frontWallModel.setupMesh(4, frontWall_pos, frontWall_tex, frontWall_nor);
		frontWallModel.setupIndices(6, frontWall_indices);

		glEnable(GL_MULTISAMPLE);
	}

	virtual void shutdown() {
		glDeleteProgram(shader_program);
		glDeleteProgram(shader_program2);
	}

	virtual void render(double currentTime) {
		if (pause) {
			previousTime = currentTime;
			return;
		}

		animationTime += currentTime - previousTime;
		previousTime = currentTime;

		const GLfloat black[] = { 0.0f, 0.0f, 0.0f, 1.0f };
		glClearBufferfv(GL_COLOR, 0, black);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		glEnable(GL_DEPTH_TEST);
		glEnable(GL_CULL_FACE);

		// 카메라 매트릭스 계산
		vmath::vec3 eye(objPosition[0], objPosition[1] + 0.5f, objPosition[2] + 2.5f);
		vmath::vec3 center(objPosition[0], objPosition[1], objPosition[2]);
		vmath::vec3 up(0.0, 1.0, 0.0);
		vmath::mat4 lookAt = vmath::lookat(eye, center, up);
		vmath::mat4 projM = vmath::perspective(fov, (float)info.windowWidth / (float)info.windowHeight, 0.1f, 1000.0f);

		// 라이팅 설정 ---------------------------------------
		vmath::vec3 pointLightPos[] = {
			vmath::vec3(0.0f, 1.25f, -11.5f),									// exit
			vmath::vec3(objPosition[0], objPosition[1] + 2.5f, objPosition[2]),	// objModel
			vmath::vec3(0.0f, 2.0f, -18.f) // chest
		};
		vmath::vec3 viewPos = eye;

		// 먹이 좌표 설정
		vmath::vec3 feedPos[] = {
			vmath::vec3(0.3f, -0.85f, -1.38f),
			vmath::vec3(0.25f, -0.85f, -5.25f),
			vmath::vec3(-0.75f, -0.85f, -3.25f),
			vmath::vec3(-0.5f, -0.85f, -6.75f),
			vmath::vec3(0.4f, -0.85f, -7.5f)
		};

		// dirLight ---------------------------------------
		glUseProgram(shader_program);

		glUniformMatrix4fv(glGetUniformLocation(shader_program, "projection"), 1, GL_FALSE, projM);
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "view"), 1, GL_FALSE, lookAt);
		glUniform3fv(glGetUniformLocation(shader_program, "viewPos"), 1, viewPos);

		glUniform3f(glGetUniformLocation(shader_program, "dirLight.direction"), 0.0f, 1.0f, 0.0f);
		glUniform3f(glGetUniformLocation(shader_program, "dirLight.ambient"), 0.05f, 0.05f, 0.05f);
		glUniform3f(glGetUniformLocation(shader_program, "dirLight.diffuse"), 0.4f, 0.4f, 0.4f);
		glUniform3f(glGetUniformLocation(shader_program, "dirLight.specular"), 0.5f, 0.5f, 0.5f);

		feedCnt = 0;
		for (int i = 0; i < 5; i++) {
			if (isFeed[i]) {
				feedCnt++;
			}
		}

		float exitLight[3] = { 1.0f, 0.0f, 0.0f };   // red
		if (feedCnt == 5) {  // green
			exitLight[0] = 0.0f;
			exitLight[1] = 1.0f;
		}

		glUniform3fv(glGetUniformLocation(shader_program, "pointLights[1].position"), 1, pointLightPos[0]);
		glUniform3f(glGetUniformLocation(shader_program, "pointLights[1].ambient"), 0.05f, 0.05f, 0.05f);
		glUniform3f(glGetUniformLocation(shader_program, "pointLights[1].diffuse"), exitLight[0], exitLight[1], exitLight[2]);
		glUniform3f(glGetUniformLocation(shader_program, "pointLights[1].specular"), exitLight[0], exitLight[1], exitLight[2]);
		glUniform1f(glGetUniformLocation(shader_program, "pointLights[1].c1"), 0.22f);
		glUniform1f(glGetUniformLocation(shader_program, "pointLights[1].c2"), 0.2f);

		vmath::vec3 yellowLight(pointLightPos[2][0], pointLightPos[2][1] - 0.5f, pointLightPos[2][2]);
		glUniform3fv(glGetUniformLocation(shader_program, "spotLight[1].position"), 1, pointLightPos[2]);
		glUniform3fv(glGetUniformLocation(shader_program, "spotLight[1].direction"), 1, yellowLight- pointLightPos[2]);
		glUniform1f(glGetUniformLocation(shader_program, "spotLight[1].cutOff"), (float)cos(vmath::radians(5.5)));
		glUniform1f(glGetUniformLocation(shader_program, "spotLight[1].outerCutOff"), (float)cos(vmath::radians(8.5)));
		glUniform1f(glGetUniformLocation(shader_program, "spotLight[1].c1"), 0.09f);
		glUniform1f(glGetUniformLocation(shader_program, "spotLight[1].c2"), 0.032f);
		glUniform3f(glGetUniformLocation(shader_program, "spotLight[1].ambient"), 0.0f, 0.0f, 0.0f);
		glUniform3f(glGetUniformLocation(shader_program, "spotLight[1].diffuse"), 1.0f, 1.0f, 0.0f);
		glUniform3f(glGetUniformLocation(shader_program, "spotLight[1].specular"), 1.0f, 1.0f, 0.0f);

		if (lineMode)
			glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
		else
			glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);

		// objModel LIGHT 그리기 ---------------------------------------
		if (drawModel) {
			// 주인공 pointLight
			glUniform3fv(glGetUniformLocation(shader_program, "pointLights[0].position"), 1, pointLightPos[1]);
			glUniform3f(glGetUniformLocation(shader_program, "pointLights[0].ambient"), 0.05f, 0.05f, 0.05f);
			glUniform3f(glGetUniformLocation(shader_program, "pointLights[0].diffuse"), 0.8f, 0.8f, 0.8f);
			glUniform3f(glGetUniformLocation(shader_program, "pointLights[0].specular"), 1.0f, 1.0f, 1.0f);
			glUniform1f(glGetUniformLocation(shader_program, "pointLights[0].c1"), 0.09f);
			glUniform1f(glGetUniformLocation(shader_program, "pointLights[0].c2"), 0.032f);

			// 주인공 spotLight
			vmath::vec3 pointLight(objPosition[0], objPosition[1] + 0.5f, objPosition[2]);
			glUniform3fv(glGetUniformLocation(shader_program, "spotLight[0].position"), 1, pointLightPos[1]);
			glUniform3fv(glGetUniformLocation(shader_program, "spotLight[0].direction"), 1, pointLight - pointLightPos[1]);
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].cutOff"), (float)cos(vmath::radians(15.5)));
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].outerCutOff"), (float)cos(vmath::radians(20.5)));
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].c1"), 0.22f);
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].c2"), 0.2f);
			glUniform3f(glGetUniformLocation(shader_program, "spotLight[0].ambient"), 0.0f, 0.0f, 0.0f);
			glUniform3f(glGetUniformLocation(shader_program, "spotLight[0].diffuse"), 1.0f, 1.0f, 1.0f);
			glUniform3f(glGetUniformLocation(shader_program, "spotLight[0].specular"), 1.0f, 1.0f, 1.0f);

			float scaleFactor = 0.05f;
			vmath::mat4 transform = vmath::translate(pointLightPos[1]) *
				vmath::scale(scaleFactor, scaleFactor, scaleFactor);
			glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, transform);

			lightModel.draw(shader_program);
		}
		else {
			// 주인공 pointLight
			glUniform3fv(glGetUniformLocation(shader_program, "pointLights[0].position"), 1, pointLightPos[1]);
			glUniform3f(glGetUniformLocation(shader_program, "pointLights[0].ambient"), 0.0f, 0.0f, 0.0f);
			glUniform3f(glGetUniformLocation(shader_program, "pointLights[0].diffuse"), 0.0f, 0.0f, 0.0f);
			glUniform3f(glGetUniformLocation(shader_program, "pointLights[0].specular"), 0.0f, 0.0f, 0.0f);
			glUniform1f(glGetUniformLocation(shader_program, "pointLights[0].c1"), 0.09f);
			glUniform1f(glGetUniformLocation(shader_program, "pointLights[0].c2"), 0.032f);

			// 주인공 spotLight
			vmath::vec3 pointLight(objPosition[0], objPosition[1] + 0.5f, objPosition[2]);
			glUniform3fv(glGetUniformLocation(shader_program, "spotLight[0].position"), 1, pointLightPos[1]);
			glUniform3fv(glGetUniformLocation(shader_program, "spotLight[0].direction"), 1, pointLight - pointLightPos[1]);
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].cutOff"), (float)cos(vmath::radians(15.5)));
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].outerCutOff"), (float)cos(vmath::radians(20.5)));
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].c1"), 0.22f);
			glUniform1f(glGetUniformLocation(shader_program, "spotLight[0].c2"), 0.2f);
			glUniform3f(glGetUniformLocation(shader_program, "spotLight[0].ambient"), 0.0f, 0.0f, 0.0f);
			glUniform3f(glGetUniformLocation(shader_program, "spotLight[0].diffuse"), 0.0f, 0.0f, 0.0f);
			glUniform3f(glGetUniformLocation(shader_program, "spotLight[0].specular"), 0.0f, 0.0f, 0.0f);
		}

		// 바닥 그리기 --------------------------------------------
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, 0.f) * vmath::scale(3.f, 6.f, 6.f));
		floorModel.draw(shader_program);

		// 왼쪽벽 그리기 --------------------------------------------
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, 0.f) * vmath::scale(3.f, 6.f, 6.f));
		leftWallModel.draw(shader_program);

		// 오른쪽벽 그리기 --------------------------------------------
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, 0.f) * vmath::scale(3.f, 6.f, 6.f));
		rightWallModel.draw(shader_program);

		// 문 그리기 --------------------------------------------
		vmath::mat4 modelDoor = vmath::translate(0.0f, -0.15f, -12.0f) *
			vmath::rotate(90.f, 0.0f, 1.0f, 0.0f) *
			vmath::scale(0.0075f);
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, modelDoor);
		door.draw(shader_program);

		// 앞 벽 그리기 --------------------------------------------
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, -9.f) * vmath::scale(3.f, 3.f, 3.f));
		frontWallModel.draw(shader_program);

		// exitSign 그리기 --------------------------------------------
		vmath::mat4 modelExit = vmath::translate(pointLightPos[0][0], pointLightPos[0][1] - 0.25f, pointLightPos[0][2] - 0.5f) *
			vmath::rotate(180.f, 0.0f, 0.0f, 1.0f) *
			vmath::scale(0.45f);
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, modelExit);
		exitSign.draw(shader_program);

		// fence 그리기 --------------------------------------------
		vmath::mat4 modelFence = vmath::translate(0.0f, -0.95f, -9.f) * vmath::scale(0.02f);
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, modelFence);
		if (feedCnt < 5) {
			fence.draw(shader_program);
		}

		// 성공시 보이는 room -----------------------------------------------
		// 바닥 
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, -13.f) * vmath::scale(3.f, 3.f, 6.f));
		floorModel.draw(shader_program);

		// 왼쪽벽
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, -13.f) * vmath::scale(3.f, 6.f, 6.f));
		leftWallModel.draw(shader_program);

		// 오른쪽벽
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, -13.f) * vmath::scale(3.f, 6.f, 6.f));
		rightWallModel.draw(shader_program);

		// 앞 벽 그리기
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, -17.f) * vmath::scale(3.f, 6.f, 3.f));
		frontWallModel.draw(shader_program);

		// 보물상자
		glUniformMatrix4fv(glGetUniformLocation(shader_program, "model"), 1, GL_FALSE, vmath::translate(0.f, -1.f, -18.f) * vmath::scale(0.5f));
		chest.draw(shader_program);

		glUseProgram(shader_program2);

		glUniformMatrix4fv(glGetUniformLocation(shader_program2, "projection"), 1, GL_FALSE, projM);
		glUniformMatrix4fv(glGetUniformLocation(shader_program2, "view"), 1, GL_FALSE, lookAt);
		glUniform3fv(glGetUniformLocation(shader_program2, "viewPos"), 1, viewPos);

		float angle = animationTime * 100;
		float side[2] = { 1.f, -1.f };
		float trans[2] = { -0.25f, 0.25f };

		// 주인공 그리기 --------------------------------------------
		if (drawModel) {
			// 지정범위라면 성공룸으로 텔레포트
			if (objPosition[0] >= -0.5f && objPosition[0] <= 0.5f
				&& objPosition[2] <= -10.5f && objPosition[2] >= -11.5f) {
				objPosition[2] = -15.f;
			}
			vmath::mat4 model = vmath::translate(objPosition) *
				vmath::rotate(180.f, 0.0f, 1.0f, 0.0f) *
				vmath::rotate(objYangle, 0.0f, 1.0f, 0.0f) *
				vmath::scale(0.5f);
			glUniformMatrix4fv(glGetUniformLocation(shader_program2, "model"), 1, GL_FALSE, model);
			objModel.draw(shader_program2);
		}
		// BodyGuard 시바견 그리기		
		for (int i = 0; i < 2; i++) {
			vmath::mat4 modelBodyGuard = { vmath::translate(side[i], -0.55f, -11.f) *
			vmath::rotate(angle * side[i], 0.0f, 1.0f, 0.0f) *
			vmath::scale(0.4f) };
			glUniformMatrix4fv(glGetUniformLocation(shader_program2, "model"), 1, GL_FALSE, modelBodyGuard);
			bodyGuard.draw(shader_program2);
		}
		// student 시바견 그리기
		for (int i = 0; i < 2; i++) {
			vmath::mat4 modelStudent = { vmath::translate(side[i] + trans[i], -0.5f, -.5f) *
			vmath::rotate(270 * side[i], 0.0f, 1.0f, 0.0f) *
			vmath::scale(0.4f) };
			glUniformMatrix4fv(glGetUniformLocation(shader_program2, "model"), 1, GL_FALSE, modelStudent);
			student.draw(shader_program2);
		}

		// 먹이 그리기 ---------------------------------------
		for (int i = 0; i < 5; i++) {
			if (objPosition[0] >= feedPos[i][0] - 0.15f && objPosition[0] <= feedPos[i][0] + 0.15f
				&& objPosition[2] >= feedPos[i][2] - 0.15f && objPosition[2] <= feedPos[i][2] + 0.15f) {
				isFeed[i] = true;
			} // 먹은 feed 처리

			if (!isFeed[i]) {
				float scaleFactor = 0.05f;
				vmath::mat4 transform = vmath::translate(feedPos[i]) *
					vmath::rotate(180.f, 0.0f, 1.0f, 0.0f) *
					vmath::scale(scaleFactor, scaleFactor, scaleFactor);
				glUniformMatrix4fv(glGetUniformLocation(shader_program2, "model"), 1, GL_FALSE, transform);
				feed.draw(shader_program2);
			} // 먹지 않은 feed만 draw
		}
	}

	virtual void onResize(int w, int h) {
		sb7::application::onResize(w, h);

		if (glViewport != NULL)
			glViewport(0, 0, info.windowWidth, info.windowHeight);
	}

	virtual void init() {
		sb7::application::init();

		info.samples = 8;
		info.flags.debug = 1;
	}

	virtual void onKey(int key, int action){
		if (action == GLFW_PRESS) {
			switch (key) {
			case ' ':
				pause = !pause;
				break;
			case 'W':
				if (!pause) {
					if(feedCnt < 5 && objPosition[2] > -9.f)
						objPosition[2] -= 0.3;
					else if (feedCnt >= 5 && objPosition[2] > -19.5f)
						objPosition[2] -= 0.3;
				}
				break;
			case 'A':
				if (!pause && objPosition[0] >= -1.f)
					objPosition[0] -= 0.3;
				break;
			case 'S':
				if (!pause) {
					if (feedCnt < 5 && objPosition[2] <= 1.5f)
						objPosition[2] += 0.3;
					else if (feedCnt >= 5 && objPosition[2] < -15.f)
						objPosition[2] += 0.3;
				}
				break;
			case 'D':
				if (!pause && objPosition[0] <= 1.f)
					objPosition[0] += 0.3;

				break;
			case '1':
				drawModel = !drawModel;
				break;
			case 'M':
				lineMode = !lineMode;
				break;
			case 'R':
				initValues();
				break;
			default:
				break;
			};
		}
	}

	virtual void onMouseButton(int button, int action) {
		if (button == GLFW_MOUSE_BUTTON_LEFT && action == GLFW_PRESS) {
			mouseDown = true;

			int x, y;
			getMousePosition(x, y);
			mousePostion = vmath::vec2(float(x), float(y));
		}
		else {
			mouseDown = false;
		}
	}

	virtual void onMouseMove(int x, int y) {
		if (mouseDown) {
			objYangle += float(x - mousePostion[0]) / 2.f;
			mousePostion = vmath::vec2(float(x), float(y));
		}
	}

#define MAX_FOV 120.f
#define MIN_FOV 10.f
	virtual void onMouseWheel(int pos) {
		if (pos > 0)
			fov = vmath::min(MAX_FOV, fov + 1.0f);
		else
			fov = vmath::max(MIN_FOV, fov - 1.0f);
	}

	void initValues() {
		drawModel = true;
		drawLight = true;
		pause = false;
		animationTime = 0;
		previousTime = 0;
		lineMode = false;

		mouseDown = false;
		wheelPos = 0;

		fov = 50.f;
		feedCnt = 0;
		isFeed[0] = false;
		isFeed[1] = false;
		isFeed[2] = false;
		isFeed[3] = false;
		isFeed[4] = false;

		objPosition = vmath::vec3(0.0f, -0.5f, 0.0f);
		objYangle = 0.f;
	}


private:
	GLuint shader_program, shader_program2;
	GLuint FBO;

	Model objModel;
	vmath::vec3 objPosition;

	Model bodyGuard, student, door, lightModel, exitSign, fence, feed, chest;
	Model floorModel, leftWallModel, rightWallModel, frontWallModel;

	float objYangle;

	bool drawModel, drawLight;
	bool lineMode;
	bool pause;

	double previousTime;
	double animationTime;

	vmath::vec2 mousePostion;
	bool mouseDown;
	int wheelPos, feedCnt;
	bool isFeed[5];
	float fov;
};

// DECLARE_MAIN의 하나뿐인 인스턴스
DECLARE_MAIN(my_application)