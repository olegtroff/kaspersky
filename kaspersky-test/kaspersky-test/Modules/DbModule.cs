using Autofac;
using Core.Repository;
using InMemoryStorage;
using Repository;

namespace kaspersky_test.Modules
{
    internal class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryStorage<PublishingHouseEntity>>()
                .As<IInMemoryStorage<PublishingHouseEntity>>()
                .SingleInstance();

            builder.RegisterType<PublishingHouseRepository>()
                .As<IPublishingHouseRepository>()
                .Named<IPublishingHouseRepository>("notCached")
                .SingleInstance();

            builder.RegisterType<InMemoryStorage<AuthorEntity>>()
                .As<IInMemoryStorage<AuthorEntity>>()
                .SingleInstance();

            builder.RegisterType<AuthorRepository>()
                .As<IAuthorRepository>()
                .Named<IAuthorRepository>("notCached")
                .SingleInstance();

            builder.RegisterType<InMemoryStorage<BookEntity>>()
                .As<IInMemoryStorage<BookEntity>>()
                .SingleInstance();

            builder.RegisterType<BookRepository>()
                .As<IBookRepository>()
                .Named<IBookRepository>("notCached")
                .SingleInstance();
        }
    }
}
