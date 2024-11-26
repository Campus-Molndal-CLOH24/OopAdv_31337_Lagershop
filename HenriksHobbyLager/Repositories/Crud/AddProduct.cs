﻿namespace HenriksHobbylager.Repositories.Crud;

private void AddProduct()
{
    Console.WriteLine("Skriv in produktens namn: ");
    var productName = Console.ReadLine();
    Console.WriteLine("Skriv in antal: ");
    var productQuantity = int.Parse(Console.ReadLine());
    Console.WriteLine("Skriv in priset på produkten: ");
    var productPrice = decimal.Parse(Console.ReadLine());
    _productFacade.AddProduct(productName, productQuantity, productPrice);
    
    // TODO: Add category ?
    // var category = Console.ReadLine();
}