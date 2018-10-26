using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using static System.Console;
using System.Security.Principal;

namespace SecurityExamples
{
    static class PermissionsBasicExamples
    {
        public static void Run()
        {
            //DemoImpersonatingThread();
            //DemoImpersonatingApp();
            //DemoGettingPermissions();
            //DemoDemandingDeclaratively();
            DemoCreatingIdentityAndPrincipals();
        }

        static void DemoGettingPermissions()
        {
            //Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent()); // or you can do the following
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            var principalPermission = new PrincipalPermission(null, "administrators");
            try
            {
                WriteLine($"Trying to get administrative permission...");
                principalPermission.Demand();
                WriteLine($"administrative access granted...");
            }
            catch (SecurityException ex)
            {
                WriteLine($"Could not get the administrative access. Trying running as administrator. Details: {ex.Message}");
            }
        }

        static void DemoImpersonatingThread()
        {
            WriteLine("Before setting:");
            var principal = Thread.CurrentPrincipal;
            WriteLine("Current Thread's Principal's primary identity: {0}", string.IsNullOrEmpty(principal.Identity.Name) ? "<NA>" : principal.Identity.Name);
            var currentUser = WindowsIdentity.GetCurrent();
            WriteLine("Setting thread's principal to Windows User: {0}", currentUser.Name);
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            WriteLine("After setting:");
            principal = Thread.CurrentPrincipal;
            WriteLine("Now Current Thread's Principal's primary identity: {0}", string.IsNullOrEmpty(principal.Identity.Name) ? "<NA>" : principal.Identity.Name);
            var currentPrincipal = WindowsPrincipal.Current;
            WriteLine($"Current Windows Principal: {currentPrincipal.Identity.Name}");
        }

        static void DemoImpersonatingApp()
        {
            WriteLine("Before setting:");
            var principal = Thread.CurrentPrincipal;
            WriteLine("Current Thread's Principal's primary identity: {0}", string.IsNullOrEmpty(principal.Identity.Name) ? "<NA>" : principal.Identity.Name);
            var currentPrincipal = WindowsPrincipal.Current;
            WriteLine($"Current Windows Principal: {currentPrincipal.Identity.Name}");

            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WriteLine("After setting:");
            principal = Thread.CurrentPrincipal;
            WriteLine("Current Thread's Principal's primary identity: {0}", string.IsNullOrEmpty(principal.Identity.Name) ? "<NA>" : principal.Identity.Name);
            currentPrincipal = WindowsPrincipal.Current;
            WriteLine($"Current Windows Principal: {currentPrincipal.Identity.Name}");
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "administrators")]
        static void MethodWithPrincipalPermissionAttribute()
        {
            WriteLine("You are executing as admin.");
        }

        static void DemoDemandingDeclaratively()
        {
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            try
            {
                MethodWithPrincipalPermissionAttribute();
            }

            catch (SecurityException ex)
            {
                WriteLine($"Could not get the administrative access. Trying running as administrator. Details: {ex.Message}");
            }
        }

        static void DemoCreatingIdentityAndPrincipals()
        {
            var identity = new GenericIdentity("Some Name");
            var principal = new GenericPrincipal(identity, new[] { "administrators" });
            Thread.CurrentPrincipal = principal;
            WriteLine($"Thread executing as identity of {Thread.CurrentPrincipal.Identity.Name}");
        }
    }
}
