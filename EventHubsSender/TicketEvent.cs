using System;
using System.Collections.Generic;
using System.Text;

namespace EventHubsSender
{
    public static class TicketEvent
    {
        public static CreateTicketMODEL Create(int ticketNumber)
        {
            try
            {
                CreateTicketMODEL ticket = new CreateTicketMODEL()
                {
                    ticket_id = ticketNumber,
                    reply = "Ticket Está sendo FECHADO para fim de testes.",
                    status = 5,
                    priority = 1,
                    source = 5,
                    type = "Service Request",
                    email = "caique.araujo@dorconsultoria.com.br",
                    responder_id = 15000576133
                };

                return ticket;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
