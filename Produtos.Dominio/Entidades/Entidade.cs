using Flunt.Notifications;

namespace Dominio.Entidades;

/// <summary>
/// Entidade base com dados comuns a todas as entidades
/// </summary>
public abstract class Entidade : Notifiable<Notification>
{
    /// <summary>
    /// Código identificador do registro
    /// </summary>
    public int Id { get; protected set; }
}