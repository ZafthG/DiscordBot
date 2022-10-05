﻿//
//                  Settings.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics
{
    /// <summary>
    /// Classe de configurações do programa.
    /// </summary>
    internal sealed class Settings
    {
        /// <summary> Delimita o máximo de tentativas antes de definir uma falha. </summary>
        public const int Services_MaxTries = 5;
        /// <summary> Tempo padrão para execução das atividades de atualização (em minutos). </summary>
        public const int Default_TimeUpdate = 1;
        /// <summary> Horário padrão para os alertas do bot. </summary>
        public const int Alert_StandardHour = 7;

        /// <summary> Servidor MySQL. </summary>
        public const string MySQL_Server = "";
        /// <summary> Usuário do MySQL. </summary>
        public const string MySQL_Uid = "";
        /// <summary> Senha do MySQL. </summary>
        public const string MySQL_Pwd = "";
        /// <summary> Nome do banco de dados MySQL. </summary>
        public const string MySQL_Database = "";
        /// <summary> Limite de tempo para a conexão ficar ausente (em minutos). </summary>
        public const int MySQL_Timeout = 10;

        /// <summary> Token de conexão para o bot do Discord. </summary>
        public const string Discord_BotToken = "";
        /// <summary> ID do bot dentro do sistema do Discord. </summary>
        public const ulong Discord_BotId = 1009139095085256796;

        /// <summary> Exibir informações de Debug? </summary>
        public const bool Debug = true;
    }
}
//
//      Fim do código