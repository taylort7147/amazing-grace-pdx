import { registerSchema } from "@message-manager/shared/schemas/auth.schema";

export function validateRegisterForm(data) {
    const parsed = registerSchema.safeParse(data);

    if (parsed.success) {
        return { valid: true, errors: {} };
    }

    const fieldErrors = {};
    for (const issue of parsed.error.issues) {
        const fieldName = issue.path[0];
        fieldErrors[fieldName] = issue.message;
    }

    return { valid: false, errors: fieldErrors };
}
