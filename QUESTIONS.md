# TesteTecnico
1. Como você implementou a função que retorna a representação por extenso
do número no desafio 1? Quais foram os principais desafios encontrados?

R: Utilizei de uma mascara de 15 digitos, aonde passo de 3 em 3 verificando se são maiores do que zero, para então nomear a centena.
O maior desafio foram os nomes, que em alguns casos tinham exeções, tipo a dezena do dez, que se não tratada ficaria dez e um, dez e dois..., e a centena do cem, que passa numa situação parecida, cem e um, cem e dois...

2. Como você lidou com a performance na implementação do desafio 2,
considerando que o array pode ter até 1 milhão de números?

R: Realizei diversos testes utilizando outros tipos de coleções, como por exemplo List<> e LinkedList<>, porém para esse exemplo, o básico se mostrou o mais eficiente, sendo a melhor opção, um ForeEach em um Array de int e o operador de agregação"+="

3. Como você lidou com os possíveis erros de entrada na implementação do
desafio 3, como uma divisão por zero ou uma expressão inválida?

R: Primeiramente com uma validação para garantir que haveriam apenas numeros ou operadores aritméticos, como a divição por Zero estavá retornando um valor infinito, para esse caso, adicionei uma tratativa manual, que retorna uma ArithmeticException.

4. Como você implementou a função que remove objetos repetidos na
implementação do desafio 4? Quais foram os principais desafios
encontrados?

Inicialmente pretendia usar o HashSet, porém como fiz o projeto baseado em API, aonde as controlers recebem seus parametros por Json, aproveitei para fazer o parametro uma lista de JsonElement, assim somando o "ToString" com o "ValueKind" consegui montar uma função que não só distingue valores, como também distingue o tipo desses valores, podendo ser utilizada tanto pra tipos de texto, numero, listas ou uma mistura de ambos