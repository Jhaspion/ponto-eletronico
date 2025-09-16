# Folha de Ponto – Academia Promoov

Este projeto é uma aplicação web em HTML/CSS/JavaScript que simula uma folha de ponto eletrônica para uma academia. A interface permite registrar a entrada e saída de colaboradores, definir feriados, gerar relatórios em PDF e exportar/importar os dados em formato JSON. Toda a lógica é executada no navegador, utilizando o localStorage para persistir dados enquanto a aba estiver aberta.

## Como executar

Este projeto é puramente front‑end e pode ser aberto diretamente no navegador sem a necessidade de um servidor. No entanto, recomenda‑se servir os arquivos via HTTP para evitar restrições relacionadas a file:// em alguns navegadores.

### Abrindo diretamente

Basta abrir o arquivo index.html com o seu navegador favorito. A aplicação deve carregar normalmente.

### Servindo via Python (opcional)

Se preferir utilizar um servidor local simples, execute o seguinte comando na raiz do projeto (é preciso ter o Python instalado):

python -m http.server --directory . 8000

Em seguida, acesse http://localhost:8000 no seu navegador para ver a aplicação.

## Funcionalidades

- Cadastro de horários: clique em qualquer dia para registrar a entrada e saída (manhã e tarde) e adicionar observações.
- Definição de feriados: marque feriados que utilizarão o horário de sábado.
- Exportação/Importação: exporte os dados em JSON para backup ou importação posterior.
- Relatórios em PDF: gere relatórios individuais ou gerais com total de horas trabalhadas por funcionário.
- Testes rápidos: botão de teste para preencher dados de forma automática e validar a aplicação.

## Observação

A aplicação utiliza o localStorage para gravar dados quando possível. Se o navegador bloquear o armazenamento (por exemplo, em modo incógnito), os dados serão mantidos apenas em memória enquanto a aba estiver aberta.

## Licença

Este projeto é distribuído sob a licença MIT. Consulte o arquivo LICENSE (não incluído) para obter detalhes.
