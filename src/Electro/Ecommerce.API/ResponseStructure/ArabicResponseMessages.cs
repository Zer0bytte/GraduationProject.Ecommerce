namespace Ecommerce.API.ResponseStructure;

public static class ArabicResponseMessages
{
    public static class Categories
    {
        public const string Created = "تم إنشاء الفئة بنجاح";
        public const string Updated = "تم تحديث الفئة بنجاح";
        public const string Deleted = "تم حذف الفئة بنجاح";
    }

    public static class Products
    {
        public const string Created = "تم إنشاء المنتج بنجاح";
        public const string Updated = "تم تحديث المنتج بنجاح";
        public const string Deleted = "تم حذف المنتج بنجاح";
        public const string ImageAdded = "تم إضافة الصورة بنجاح";
        public const string ImageDeleted = "تم حذف الصورة بنجاح";
    }

    public static class Orders
    {
        public const string Created = "تم إنشاء الطلب بنجاح";
        public const string Cancelled = "تم الغاء طلب هذا العنصر بنجاح";
        public const string StatusUpdated = "تم تحديث حالة الطلب بنجاح";
    }

    public static class Authentication
    {
        public const string RegisterSuccess = "تم التسجيل بنجاح، يرجى إدخال رمز التأكيد";
        public const string LoginSuccess = "تم تسجيل الدخول بنجاح";
        public const string SupplierLoginSuccess = "تم تسجيل دخول المورد بنجاح";
    }

    public static class Cart
    {
        public const string AddedToCart = "تم إضافة المنتج إلى السلة";
        public const string UpdatedCart = "تم تحديث السلة بنجاح";
    }

    public static class Reviews
    {
        public const string Created = "شكراً لك، تم إضافة التقييم بنجاح";
    }

    public static class Addresses
    {
        public const string Created = "تم إضافة العنوان بنجاح";
        public const string Updated = "تم تحديث العنوان بنجاح";
        public const string Deleted = "تم حذف العنوان بنجاح";
    }

    public static class Suppliers
    {
        public const string Verified = "تم التحقق من المورد بنجاح";
        public const string Rejected = "تم رفض المورد بنجاح";
    }

    public static class AdminUsers
    {
        public const string Created = "تم إنشاء الحساب وتم إرسال رابط إعادة تعيين كلمة المرور";
        public const string Deactivated = "تم تعطيل المشرف بنجاح";
        public const string Reactivated = "تم تنشيط المشرف بنجاح";
    }

    public static class CouponCodes
    {
        public const string Created = "تم إنشاء كود الخصم بنجاح";
        public const string Valid = "كود الخصم صالح";
        public const string Deactivated = "تم الغاء تنشيط هذا الكود بنجاح";
        public const string Reactivated = "تم تنشيط هذا الكود بنجاح";
    }

    public static class Conversations
    {
        public const string Started = "تم بدء المحادثة بنجاح";
    }
}