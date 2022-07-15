namespace MediX.Models
{
    public static class Roles
    {
        public static string Admin = "Admin";
        public static string Customer = "Customer";
        public static string Pharmacist = "Pharmacist";
        public static string InventoryManager = "Inventory Manager";
        public static string OrderManager = "Order Manager";

        public static bool IsAdmin(string s) { return s == Admin; }
        public static bool IsCustomer(string s) { return s == Customer; }
        public static bool IsPharmacist(string s) { return s == Pharmacist; }
        public static bool IsInventoryManager(string s) { return s == InventoryManager; }
        public static bool IsOrderManager(string s) { return s == OrderManager; }
        public static bool IsAValidRole(string s) { return (IsAdmin(s) || IsCustomer(s) || IsPharmacist(s) || IsInventoryManager(s) || IsOrderManager(s)); }
    }
}