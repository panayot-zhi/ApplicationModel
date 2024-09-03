# Application Model

As with the backend, you can mix and match from the features you want to use. But there is a convenience wrapper that will help you configure it all in
the form of a custom component that provides the `ApplicationModelContext` and configures other application model contexts in one go.

However, if you're looking to use some of the microservice capabilities, you will have to use the `ApplicationModelContext` to provide the name of the
currently running microservice. Internally, the application model uses this information to add the correct headers / query string parameters to distinguish
one microservice from the other in a composition with a single ingress in front of it.

To add the application model, you simply add the following to your application:

```tsx
export const App = () => {
    return (
        <ApplicationModel microservice='{the name of your microservice}'>
            {/* Your application */}
        </ApplicationModel>
    );
};
```
