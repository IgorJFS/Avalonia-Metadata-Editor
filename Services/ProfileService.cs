using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Platform;
using Docs_Metadata_Editor.Models;

namespace Docs_Metadata_Editor.Services;

/// <summary>
/// Service implementation for retrieving ATS profiles.
/// </summary>
public class ProfileService : IProfileService
{
    public Task<IEnumerable<AtsProfile>> GetProfilesAsync()
    {
        return Task.Run<IEnumerable<AtsProfile>>(() =>
        {
            try
            {
                var uri = new Uri("avares://Docs_Metadata_Editor/Assets/profiles.json");
                using var stream = AssetLoader.Open(uri);
                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var profiles = JsonSerializer.Deserialize<List<AtsProfile>>(json, options);
                return profiles ?? GetDefaultProfiles();
            }
            catch (Exception ex)
            {
                // Fallback to hardcoded default profiles in case of asset loading failure.
                System.Diagnostics.Debug.WriteLine($"Failed to load profiles.json: {ex.Message}");
                return GetDefaultProfiles();
            }
        });
    }

    private IEnumerable<AtsProfile> GetDefaultProfiles()
    {
        return new List<AtsProfile>
        {
            // ================= EN ENGLISH PROFILES =================
            new AtsProfile
            {
                ProfileId = "frontend_intern_en",
                Language = "EN",
                RoleName = "Frontend Developer - Intern",
                Title = "Frontend Developer Intern",
                Subject = "Frontend Web Development, Responsive UI Design, Modern JavaScript & Vue.js Foundations",
                Keywords = "HTML5, CSS3, JavaScript, ES6, Vue.js, Tailwind CSS, Git, Responsive Design, Docker, Web Accessibility",
                Comments = "Detail-oriented Frontend Intern skilled in building modern interfaces with Vue.js and Tailwind CSS. Eager to contribute to web applications while utilizing Docker for local development."
            },
            new AtsProfile
            {
                ProfileId = "frontend_jr_en",
                Language = "EN",
                RoleName = "Frontend Developer - Junior",
                Title = "Junior Frontend Developer",
                Subject = "Single Page Application Development, Component Libraries, UI Styling, Frontend Tooling",
                Keywords = "JavaScript, TypeScript, Vue.js, React, Tailwind CSS, Vite, HTML5, CSS3, Git, Docker, REST APIs, UI/UX",
                Comments = "Junior Frontend Developer with practical experience in Vue.js, React, and Tailwind CSS. Proficient in styling responsive interfaces and managing local build processes containerized with Docker."
            },
            new AtsProfile
            {
                ProfileId = "frontend_pl_en",
                Language = "EN",
                RoleName = "Frontend Developer - Pleno",
                Title = "Frontend Developer (Pleno)",
                Subject = "SPA Development, State Management, Component Libraries, Frontend Architecture & Integration",
                Keywords = "TypeScript, Vue.js, Pinia, Vue Router, React, Tailwind CSS, Jest, Vite, RESTful APIs, Git, Docker, Agile/Scrum",
                Comments = "Mid-level frontend engineer with a strong track record of developing responsive single-page applications. Experienced in Vue.js/React ecosystems, Pinia/Redux state management, Tailwind CSS layouts, and containerized deployment with Docker."
            },
            new AtsProfile
            {
                ProfileId = "frontend_sr_en",
                Language = "EN",
                RoleName = "Frontend Developer - Senior",
                Title = "Senior Frontend Architect / Lead",
                Subject = "Frontend Architecture, Performance Optimization, Design Systems, Team Mentorship",
                Keywords = "TypeScript, Vue.js, Nuxt.js, Pinia, Tailwind CSS, React, Next.js, Micro-Frontends, Docker, CI/CD, Jest, Cypress, Design Systems, Web Performance, SSR/SSG",
                Comments = "Senior frontend specialist and technical architect. Expert in Nuxt.js/Next.js architectures, complex Tailwind CSS layouts, design system engineering, Docker containerization, CI/CD automation, and guiding development teams."
            },
            new AtsProfile
            {
                ProfileId = "backend_intern_en",
                Language = "EN",
                RoleName = "Backend Developer - Intern",
                Title = "Backend Developer Intern",
                Subject = "Server-Side Development, Object-Oriented Programming, Database Fundamentals, API Integration",
                Keywords = "C#, .NET Core, ASP.NET Web API, SQL Server, Entity Framework, OOP, Git, Docker, Relational Databases, REST APIs",
                Comments = "Enthusiastic Backend Intern with academic experience in C# and .NET Core. Eager to design APIs, learn relational database management, and implement microservices utilizing Docker containers."
            },
            new AtsProfile
            {
                ProfileId = "backend_jr_en",
                Language = "EN",
                RoleName = "Backend Developer - Junior",
                Title = "Junior Backend Developer",
                Subject = "Backend API Development, Database Modeling, Unit Testing, Server-Side Architectures",
                Keywords = "C#, .NET, ASP.NET Core, EF Core, SQL Server, PostgreSQL, Git, Docker, Unit Testing, xUnit, RESTful APIs, JSON",
                Comments = "Junior Backend Developer specialized in building scalable APIs using C# and ASP.NET Core. Proficient in database migrations, writing unit tests, and coordinating local environments with Docker."
            },
            new AtsProfile
            {
                ProfileId = "backend_pl_en",
                Language = "EN",
                RoleName = "Backend Developer - Pleno",
                Title = "Backend Developer (Pleno)",
                Subject = "Microservices Architecture, RESTful Web API Engineering, Database Optimization, Message Queues",
                Keywords = "C#, .NET, ASP.NET Core, EF Core, SQL Server, PostgreSQL, Redis, Docker, RabbitMQ, xUnit, CI/CD, Microservices, REST APIs",
                Comments = "Mid-level backend developer with experience in robust RESTful API design. Skilled in ORM mapping, relational databases, distributed caching, messaging systems, and containerizing backend services using Docker."
            },
            new AtsProfile
            {
                ProfileId = "backend_sr_en",
                Language = "EN",
                RoleName = "Backend Developer - Senior",
                Title = "Senior Backend Architect / Tech Lead",
                Subject = "Distributed Systems, Microservices, Cloud Native Architecture, System Scalability, Messaging & Event Streaming",
                Keywords = "C#, .NET Core, ASP.NET Core, EF Core, Microservices, Docker, Kubernetes, RabbitMQ, Kafka, AWS, Azure, PostgreSQL, Redis, Clean Architecture, CI/CD, DDD, CQRS",
                Comments = "Senior Backend Architect specializing in high-performance distributed systems. Expert in microservices scaling, event-driven architectures with RabbitMQ/Kafka, Docker/Kubernetes deployment, AWS cloud integrations, and clean code practices."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_intern_en",
                Language = "EN",
                RoleName = "Fullstack Developer - Intern",
                Title = "Fullstack Developer Intern",
                Subject = "Fullstack Web Development, Client-Server Architectures, Database Foundations, Modern Styling",
                Keywords = "JavaScript, Vue.js, Tailwind CSS, Node.js, Express, SQL, Git, Docker, Responsive Layouts, HTML5/CSS3",
                Comments = "Energetic Fullstack Intern. Eager to work across the stack building responsive UIs in Vue.js and Tailwind CSS, and designing server APIs with Node.js/Express, while orchestrating environments using Docker."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_jr_en",
                Language = "EN",
                RoleName = "Fullstack Developer - Junior",
                Title = "Junior Fullstack Developer",
                Subject = "Full-Stack Web Engineering, Database Integration, API Design, Responsive Client App",
                Keywords = "JavaScript, TypeScript, Vue.js, Tailwind CSS, Node.js, Express, C#, .NET, EF Core, SQL Server, Git, Docker, REST APIs",
                Comments = "Junior Fullstack Developer. Capable of constructing frontend pages in Vue.js with Tailwind CSS, writing backend APIs using Node.js/ASP.NET Core, and deploying unified systems containerized with Docker."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_pl_en",
                Language = "EN",
                RoleName = "Fullstack Developer - Pleno",
                Title = "Fullstack Developer (Pleno)",
                Subject = "Fullstack Application Architecture, API Integration, Relational & NoSQL Databases, DevOps Pipelines",
                Keywords = "TypeScript, Vue.js, Nuxt.js, Tailwind CSS, Node.js, Express, C#, ASP.NET Core, EF Core, SQL Server, PostgreSQL, Docker, Jest, CI/CD, REST APIs",
                Comments = "Mid-level Fullstack Developer. Experienced in Vue.js/React SPA ecosystems styled with Tailwind CSS, backends using Node.js or .NET Core, SQL databases, Docker environments, and automated CI/CD deployments."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_sr_en",
                Language = "EN",
                RoleName = "Fullstack Developer - Senior",
                Title = "Senior Fullstack Architect",
                Subject = "Enterprise Web Applications, Distributed Services, Cloud Engineering, Frontend & Backend Architecture",
                Keywords = "TypeScript, Vue.js, Nuxt.js, Tailwind CSS, Node.js, NestJS, C#, .NET Core, Microservices, Docker, Kubernetes, AWS, SQL Server, PostgreSQL, Redis, CI/CD",
                Comments = "Senior Fullstack Architect with extensive experience designing complex, high-traffic systems. Expert in Vue.js/React frontend frameworks, Tailwind CSS styling, .NET/Node.js backends, Microservices, Docker, Kubernetes orchestration, and cloud native architectures."
            },

            // ================= PT-BR PORTUGUESE PROFILES =================
            new AtsProfile
            {
                ProfileId = "frontend_intern_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Frontend - Estagiario",
                Title = "Estagiario de Desenvolvimento Frontend",
                Subject = "Desenvolvimento Web Frontend, Design de Interface Responsiva, Fundamentos de JavaScript e Vue.js",
                Keywords = "HTML5, CSS3, JavaScript, ES6, Vue.js, Tailwind CSS, Git, Design Responsivo, Docker, Acessibilidade Web",
                Comments = "Estagiario de Frontend focado e detalhista. Conhecimentos basicos em Vue.js e Tailwind CSS para interfaces web responsivas e modernas. Experiencia inicial com Docker para conteinerizacao local."
            },
            new AtsProfile
            {
                ProfileId = "frontend_jr_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Frontend - Junior",
                Title = "Desenvolvedor Frontend Junior",
                Subject = "Desenvolvimento SPA, Bibliotecas de Componentes, Estilizacao de Interfaces, Ferramentas de Build",
                Keywords = "JavaScript, TypeScript, Vue.js, React, Tailwind CSS, Vite, HTML5, CSS3, Git, Docker, APIs REST, UI/UX",
                Comments = "Desenvolvedor Frontend Junior com experiencia em Vue.js, React e estilizacao com Tailwind CSS. Habilidade na criacao de interfaces responsivas e uso de ambientes dockerizados."
            },
            new AtsProfile
            {
                ProfileId = "frontend_pl_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Frontend - Pleno",
                Title = "Desenvolvedor Frontend Pleno",
                Subject = "Desenvolvimento SPA, Gerenciamento de Estado, Bibliotecas de UI, Performance Frontend",
                Keywords = "TypeScript, Vue.js, Pinia, Vue Router, React, Tailwind CSS, Jest, Vite, APIs RESTful, Git, Docker, Metodos Ageis",
                Comments = "Engenheiro frontend pleno com historico na criacao de SPAs responsivas. Experiencia no ecossistema Vue.js/React, gerenciamento de estado com Pinia/Redux, layouts com Tailwind CSS e deploy em containers Docker."
            },
            new AtsProfile
            {
                ProfileId = "frontend_sr_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Frontend - Senior",
                Title = "Arquiteto Frontend Senior / Lead",
                Subject = "Arquitetura Frontend, Otimizacao de Performance, Design Systems, Mentoria Tecnica",
                Keywords = "TypeScript, Vue.js, Nuxt.js, Pinia, Tailwind CSS, React, Next.js, Micro-Frontends, Docker, CI/CD, Jest, Cypress, Design Systems, Web Performance, SSR/SSG",
                Comments = "Especialista frontend senior e arquiteto de software. Experiencia solida em arquiteturas Nuxt.js/Next.js, layouts complexos com Tailwind CSS, design systems, ambientes Docker, esteiras CI/CD e lideranca tecnica."
            },
            new AtsProfile
            {
                ProfileId = "backend_intern_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Backend - Estagiario",
                Title = "Estagiario de Desenvolvimento Backend",
                Subject = "Desenvolvimento Server-Side, Programacao Orientada a Objetos, Banco de Dados, Integracao de APIs",
                Keywords = "C#, .NET Core, ASP.NET Web API, SQL Server, Entity Framework, POO, Git, Docker, Bancos de Dados Relacionais, APIs REST",
                Comments = "Estagiario Backend entusiasmado com fundamentos em C# e .NET Core. Interessado em desenvolvimento de APIs, modelagem relacional e ecossistemas com Docker."
            },
            new AtsProfile
            {
                ProfileId = "backend_jr_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Backend - Junior",
                Title = "Desenvolvedor Backend Junior",
                Subject = "Desenvolvimento de APIs Backend, Modelagem de Bancos de Dados, Testes Unitarios",
                Keywords = "C#, .NET, ASP.NET Core, EF Core, SQL Server, PostgreSQL, Git, Docker, Testes Unitarios, xUnit, APIs RESTful, JSON",
                Comments = "Desenvolvedor Backend Junior focado em APIs escalaveis com C# e ASP.NET Core. Proficiente em banco de dados relacional, escrita de testes e orquestracao de ambiente local com Docker."
            },
            new AtsProfile
            {
                ProfileId = "backend_pl_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Backend - Pleno",
                Title = "Desenvolvedor Backend Pleno",
                Subject = "Arquitetura de Microservicos, APIs RESTful, Otimizacao de Queries, Mensageria",
                Keywords = "C#, .NET, ASP.NET Core, EF Core, SQL Server, PostgreSQL, Redis, Docker, RabbitMQ, xUnit, CI/CD, Microservicos, APIs REST",
                Comments = "Desenvolvedor backend de nivel pleno com solida experiencia em APIs RESTful. Conhecimentos em modelagem relacional, caching distribuido, RabbitMQ e conteinerizacao de servicos com Docker."
            },
            new AtsProfile
            {
                ProfileId = "backend_sr_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Backend - Senior",
                Title = "Arquiteto Backend Senior / Tech Lead",
                Subject = "Sistemas Distribuidos, Microsservicos, Arquitetura Cloud Native, Escabilidade, Mensageria & Streams",
                Keywords = "C#, .NET Core, ASP.NET Core, EF Core, Microsservicos, Docker, Kubernetes, RabbitMQ, Kafka, AWS, Azure, PostgreSQL, Redis, Clean Architecture, CI/CD, DDD, CQRS",
                Comments = "Arquiteto backend senior especialista em sistemas distribuidos e de alta performance. Dominio em microsservicos, arquitetura orientada a eventos (RabbitMQ/Kafka), containers com Docker/Kubernetes, AWS e boas praticas de arquitetura limpa."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_intern_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Fullstack - Estagiario",
                Title = "Estagiario de Desenvolvimento Fullstack",
                Subject = "Desenvolvimento Web Fullstack, Arquitetura Cliente-Servidor, Banco de Dados, Estilizacao Moderna",
                Keywords = "JavaScript, Vue.js, Tailwind CSS, Node.js, Express, SQL, Git, Docker, Design Responsivo, HTML5/CSS3",
                Comments = "Estagiario Fullstack motivado. Conhecimento para atuar no frontend com Vue.js e Tailwind CSS, e no backend com Node.js/Express, utilizando Docker para gerenciar os ambientes."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_jr_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Fullstack - Junior",
                Title = "Desenvolvedor Fullstack Junior",
                Subject = "Engenharia Web Fullstack, Integracao de Banco de Dados, Construcao de APIs, Cliente Responsivo",
                Keywords = "JavaScript, TypeScript, Vue.js, Tailwind CSS, Node.js, Express, C#, .NET, EF Core, SQL Server, Git, Docker, APIs REST",
                Comments = "Desenvolvedor Fullstack Junior capacitado a construir telas com Vue.js/Tailwind CSS, APIs em Node.js ou C#/.NET Core, com suporte ao desenvolvimento dockerizado."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_pl_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Fullstack - Pleno",
                Title = "Desenvolvedor Fullstack Pleno",
                Subject = "Arquitetura Fullstack, Integracao de APIs, Bancos Relacionais e NoSQL, CI/CD",
                Keywords = "TypeScript, Vue.js, Nuxt.js, Tailwind CSS, Node.js, Express, C#, ASP.NET Core, EF Core, SQL Server, PostgreSQL, Docker, Jest, CI/CD, APIs REST",
                Comments = "Desenvolvedor Fullstack Pleno com experiencia no ecossistema Vue.js/Tailwind CSS, backends Node.js e .NET Core, bancos SQL, orquestracao local com Docker e integracoes CI/CD."
            },
            new AtsProfile
            {
                ProfileId = "fullstack_sr_pt",
                Language = "PT-BR",
                RoleName = "Desenvolvedor Fullstack - Senior",
                Title = "Arquiteto Fullstack Senior",
                Subject = "Aplicacoes Corporativas, Servicos Distribuidos, Engenharia Cloud, Arquitetura Front e Back",
                Keywords = "TypeScript, Vue.js, Nuxt.js, Tailwind CSS, Node.js, NestJS, C#, .NET Core, Microsservicos, Docker, Kubernetes, AWS, SQL Server, PostgreSQL, Redis, CI/CD",
                Comments = "Arquiteto Fullstack Senior com vasta bagagem tecnica. Larga experiencia no ecossistema Vue.js/Tailwind CSS, backends robustos em C#/.NET e Node.js/NestJS, microsservicos, dockerizacao, orquestracao Kubernetes e deploy em nuvem (AWS)."
            }
        };
    }
}
