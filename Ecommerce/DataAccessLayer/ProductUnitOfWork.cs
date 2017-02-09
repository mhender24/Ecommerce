﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.DataAccessLayer
{
    public class ProductUnitOfWork
    {
        private ProductRepository productRepository;
        private SupplierRepository supplierRepository;
        private CategoryRepository categoryRepository;
        private ProductCategoryRepository productCategoryRepository;
        private Data context = new Data();

        public ProductRepository ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        public ProductCategoryRepository ProductCategoryRepository
        {
            get
            {

                if (this.productCategoryRepository == null)
                {
                    this.productCategoryRepository = new ProductCategoryRepository(context);
                }
                return productCategoryRepository;
            }
        }

        public SupplierRepository SupplierRepository
        {
            get
            {

                if (this.supplierRepository == null)
                {
                    this.supplierRepository = new SupplierRepository(context);
                }
                return supplierRepository;
            }
        }

        public CategoryRepository CategoryRepository
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}