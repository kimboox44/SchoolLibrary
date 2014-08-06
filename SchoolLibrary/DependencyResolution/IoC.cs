// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using StructureMap;
namespace SchoolLibrary.DependencyResolution {

    using SchoolLibrary.BusinessLogic.Managers;
    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.Controllers.WebAPIControllers;
    using SchoolLibrary.DataAccess.Context;
    using SchoolLibrary.DataAccess.Facades;
    using SchoolLibrary.DataAccess.Facades.Interfaces;
    using SchoolLibrary.DataAccess.UnitOfWork;

    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            x.For<ILibraryContext>().Use<LibraryContext>();
                            x.For<IUserManager>().Use<UserManager>();
                            x.For<ILibraryUow>().Use<LibraryUow>();
                            x.For<IReaderManager>().Use<ReaderManager>();
                            x.For<IReaderHistoryManager>().Use<ReaderHistoryManager>();
                            x.For<IUsersFacade>().Use<UsersFacade>();
                            x.For<ISearchBookFacade>().Use<SearchBookFacade>();
                            x.For<ISearchBookManager>().Use<SearchBookManager>();
                            x.For<IReservedItemsFacade>().Use<ReservedItemsFacade>();
                            x.For<IReservedItemManager>().Use<ReservedItemManager>();
                            x.For<IReaderFacade>().Use<ReaderFacade>();
                            x.For<IBookFacade>().Use<BookFacade>();
                            x.For<IBookManager>().Use<BookInfoManager>();
                            x.For<IAuthorFacade>().Use<AuthorFacade>();
                            x.For<IAuthorManager>().Use<AuthorManager>();
                            x.For<IBookAuthorManager>().Use<BookAuthorManager>();
                            x.For<IBookAuthorFacade>().Use<BookAuthorFacade>();
                            x.For<IBookWithAuthorsShortFacade>().Use<BookWithAuthorsShortFacade>();
                            x.For<IReaderHistoryFacade>().Use<ReaderHistoryFacade>();
                            x.For<IInventoryFacade>().Use<InventoryFacade>();
                            x.For<IInventoryManager>().Use<InventoryManager>();
                            x.For<IExcelManager>().Use<ExcelManager>();
                            x.For<IScannedPageManager>().Use<ScannedPageManager>();
                            x.For<IScannedPageFacade>().Use<ScannedPageFacade>();
                            x.For<ITagsFacade>().Use<TagsFacade>();
                            x.For<ITagsManager>().Use<TagsManager>();
                            x.For<IConsignmentManager>().Use<ConsignmentManager>();
                            x.For<IConsignmentFacade>().Use<ConsignmentFacade>();
                            x.For<IMagazineFacade>().Use<MagazineFacade>();
                            x.For<IMagazineManager>().Use<MagazineManager>();
                            x.For<IDiskFacade>().Use<DiskFacade>();
                            x.For<IDiskManager>().Use<DiskManager>();  
                            x.For<IItemFacade>().Use<ItemFacade>();
                            x.For<IItemManager>().Use<ItemManager>();
                            x.For<ISearchItemFacade>().Use<SearchItemFacade>();
                            x.For<ISearchItemManager>().Use<SearchItemManager>();
                            x.For<IRecommendationManager>().Use<RecommendationManager>();
                            x.For<ITagScoresFacade>().Use<TagScoresFacade>();
                            
                            //                x.For<IExample>().Use<Example>();
                        });
            return ObjectFactory.Container;
        }
    }
}