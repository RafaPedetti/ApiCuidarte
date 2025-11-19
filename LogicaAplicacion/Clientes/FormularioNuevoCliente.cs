using LogicaAplicacion.Dtos.Clientes;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesServicios;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace LogicaAplicacion.Clientes
{
	public class FormularioNuevoCliente : IFormularioNuevoCliente<ClienteFormularioDto>
	{
		public async void Ejecutar(ClienteFormularioDto obj)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Cuidarte", "rafapedetti@gmail.com"));
			message.To.Add(new MailboxAddress("", "rafapedetti@gmail.com"));
			message.Subject = $"Nuevo mensaje de {obj.nombre} {obj.apellido}";

			var builder = new BodyBuilder
			{
				TextBody = $@"
            Nombre: {obj.nombre} {obj.apellido}
            Email: {obj.email}
            Teléfono: {obj.telefono}
			Fecha de Nacimiento: {obj.fechaNacimiento.ToShortDateString()}
			Dirección: {obj.direccion}
			CI: {obj.ci}
			Responsable de Pago: {obj.responsablePago}
			Forma de Pago: {obj.formaPago}
			Observaciones: {obj.observaciones}
			Servicios Solicitados: {string.Join(Environment.NewLine, obj.servicios.Select(s => $"- {s.servicio}: {s.horas} horas"))}

        "
			};

			message.Body = builder.ToMessageBody();

			using var client = new SmtpClient();
			await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
			await client.AuthenticateAsync("rafapedetti@gmail.com", "vaoe uebk vznq lbcj");
			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}


	}
}