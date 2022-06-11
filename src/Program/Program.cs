using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Library;
using Telegram.Bot.Types.Enums;
using System.IO;
using System.Text;

namespace Program
{
    class Program
    {
        public static void Main()
        {

            //Obtengo una instancia de TelegramBot
            TelegramBot telegramBot = TelegramBot.Instance;
            Console.WriteLine($"Hola soy el Bot de P2, mi nombre es {telegramBot.BotName} y tengo el Identificador {telegramBot.BotId}");

            //Obtengo el cliente de Telegram
            ITelegramBotClient bot = telegramBot.Client;

            //Asigno un gestor de mensajes
            bot.OnMessage += OnMessage;

            //Inicio la escucha de mensajes
            bot.StartReceiving();


            Console.WriteLine("Presiona una tecla para terminar");
            Console.ReadKey();

            //Detengo la escucha de mensajes 
            bot.StopReceiving();
        }

        private static async void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            Chat chatInfo = message.Chat;
            string messageText = message.Text.ToLower();
            if (messageText != null)
            {
                ITelegramBotClient client = TelegramBot.Instance.Client;
                Console.WriteLine($"{chatInfo.FirstName}: envío {message.Text}");

                switch (messageText)
                {
                    case "/commands":
                    case "/comandos":
                        StringBuilder commandsStringBuilder = new StringBuilder("Lista de Comandos:\n")
                                                                            .Append("/commands\n")
                                                                            .Append("/comandos\n")
                                                                            .Append("/hola\n")
                                                                            .Append("/hello\n")
                                                                            .Append("/hi\n")
                                                                            .Append("/foto\n")
                                                                            .Append("/photo\n")
                                                                            .Append("/sticker\n")
                                                                            .Append("/link\n")
                                                                            .Append("/voice\n")
                                                                            .Append("/audio\n");


                        await client.SendTextMessageAsync(
                                                  chatId: chatInfo.Id,
                                                   text: commandsStringBuilder.ToString());
                        break;
                    case "/hola":
                    case "/hello":
                    case "/hi":
                        await client.SendTextMessageAsync(
                                          chatId: chatInfo.Id,
                                          text: $"Hola, ¿cómo estás {chatInfo.FirstName}? 👋😀");
                        break;

                    case "/foto":
                    case "/photo":
                        using (Stream stream = System.IO.File.OpenRead("../../Assets/kotlin.jpg"))
                        {
                            await client.SendPhotoAsync(
                                            chatId: chatInfo.Id,
                                            photo: stream,
                                            caption: "<b>Kotlin</b>. <i>Source</i>: <a href=\"https://www.meme-arsenal.com/en/create/meme/497325\">Meme Arsenal</a>",
                                            parseMode: ParseMode.Html);
                        }
                        break;

                    case "/sticker":
                        using (Stream stream = System.IO.File.OpenRead("../../Assets/csharp.webp"))
                        {
                            await client.SendStickerAsync(
                                                chatId: chatInfo.Id,
                                                sticker: stream);
                        }

                        break;
                    case "/link":
                        using (Stream stream = System.IO.File.OpenRead("../../Assets/ACDC_Back_In_Black.ogg"))
                        {
                            await client.SendTextMessageAsync(
                                                chatId: chatInfo.Id,
                                                text: "https://www.youtube.com/watch?v=A6ZqNQdJPjc");
                        }

                        break;
                    case "/voice":
                        using (Stream stream = System.IO.File.OpenRead("../../Assets/ACDC_Back_In_Black.ogg"))
                        {
                            await client.SendVoiceAsync(
                                                chatId: chatInfo.Id,
                                                voice: stream,

                                                duration: 25);
                        }

                        break;

                    case "/audio":
                        using (Stream stream = System.IO.File.OpenRead("../../Assets/darthVader.mp3"))
                        {
                            await client.SendAudioAsync(
                                                chatId: chatInfo.Id,
                                                title: "I'm your father",
                                                performer: "Darth Vader",
                                                thumb: "https://acib.es/wp-content/uploads/2015/11/arton427.jpg",
                                                audio: stream,
                                                duration: 3);
                        }

                        break;
                    default:
                        await client.SendTextMessageAsync(
                                              chatId: chatInfo.Id,
                                              text: $"{chatInfo.FirstName}, no comprendo lo que dices 😕"
                                            );
                        break;
                }

            }
        }
    }
}
