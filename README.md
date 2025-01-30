# Necessidades do projeto
- [x] O resultado da consulta no programa deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40** - Esta no campo message do retorno do endpoint
- [x] Registro de novas rotas. Essas novas rotas devem ser persistidas para futuras consultas
- [x] Consulta de melhor rota entre dois pontos
- [x] Implementação de testes unitários
- [x] Rotas iniciais

## Rotas Iniciais
### [POST] /Routes
Registra nova rota
```
curl -X 'POST' \
  'http://localhost:5178/Routes' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "origin": "string",
  "destination": "string",
  "cost": 0
}'
```
### [GET] /Routes/best-route?origin=[GRU]&destination=[SCL]
Calcula a rota mais barata entre dois pontos considerando todas as conexões disponíveis, recebendo o retorno:
```
curl -X 'GET' \
  'http://localhost:5178/Routes/best-route?origin=GRU&destination=SCL' \
  -H 'accept: application/json'
```
RESPONSE:
```
{
  "route": "GRU - BRC - SCL",
  "message": "GRU - BRC - SCL ao custo de $15,0",
  "totalCost": 15
}
```

## Configuração do Banco de Dados

O sistema usa SQLite para persistência. Na primeira execução:
1. Um arquivo `routes.db` será criado automaticamente
2. As rotas iniciais serão populadas

Para implantação em produção:
- Mantenha o arquivo `routes.db` no mesmo diretório do executável
- Garanta permissões de escrita no diretório
### Dados Iniciais
```
GRU,BRC,10
BRC,SCL,5
GRU,CDG,75
GRU,SCL,20
GRU,ORL,56
ORL,CDG,5
SCL,ORL,20
```

# Decisão de Design

O sistema foi projetado seguindo a **arquitetura em camadas**, separando responsabilidades para facilitar manutenção, escalabilidade e testes. As principais decisões foram:  

### **1. Camadas bem definidas**  
- **WebApi**: Responsável pela interface REST (Controllers).  
- **App (Serviços)**: Contém a lógica de negócio e regras de aplicação.  
- **Domain (Modelos e Interfaces)**: Define entidades, contratos e abstrações.  
- **Repository (Persistência)**: Implementa acesso ao banco de dados SQLite.  

### **2. Persistência com SQLite**  
- Escolhido por ser leve e embutido, facilitando testes e deploy simples.  
- As rotas são carregadas na inicialização e podem ser persistidas para consultas futuras.  

### **3. Boas práticas de desenvolvimento**  
- Uso de **injeção de dependência** para desacoplar componentes.  
- **Testes unitários** para garantir a confiabilidade da lógica de cálculo de rotas.  
- Uso de **Swagger** para documentação e testes da API.  

Essa abordagem garante um sistema modular, testável e de fácil manutenção. 🚀