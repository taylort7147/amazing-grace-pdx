import api from "../../api";
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import React, { useEffect, useState, forwardRef } from "react";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";
import { Box, Button, Flex, Heading, HStack, VStack, Input, Field, Fieldset, Textarea, Separator, Clipboard, Show } from "@chakra-ui/react";
import DateField from "../../components/DateField";
import DeleteDialog from "../../components/DeleteDialog";
import DurationInput from "../../components/DurationInput";
import SeriesComboBox from "../../components/SeriesComboBox";
import { messageSchema, notesSchema, audioSchema, videoSchema } from "@message-manager/shared/schemas/index.js"



function formatTitle(title) {
  return title ?? "";
}

function formatDescription(description) {
  return description ?? "";
}

function _StackSeparator() {
  return <Box height={50} />;
}

const ClipboardButton = forwardRef(({ value }, ref) => {
  return (
    <Clipboard.Root value={value}>
      <Clipboard.Trigger asChild>
        <Button variant="surface" size="sm" height="100%">
          <Clipboard.Indicator />
        </Button>
      </Clipboard.Trigger>
    </Clipboard.Root>
  );
});

const TextBox = forwardRef(({ value, ...props }, ref) => {
  return (
    <Textarea value={value}
      autoresize
      {...props}
      ref={ref} />
  );
});


const CopyableTextBox = forwardRef(({ value, ...props }, ref) => {
  return (
    <HStack align="stretch" w="100%">
      <TextBox value={value} {...props} ref={ref} />
      <ClipboardButton value={value} />
    </HStack>
  );
});

const CopyableTextBoxController = forwardRef(({ name, form, ...props }, ref) => {
  const { control } = form;
  return (
    <Controller
      control={control}
      name={name}
      render={({ field: { value, onChange } }) => (
        <CopyableTextBox value={value} onChange={onChange} {...props} ref={ref} />
      )}
    />
  );
});

const FormField = forwardRef(({ form, label, name, helpText, required, children }, ref) => {
  const { formState: { errors } } = form;
  const fieldError = name.split('.').reduce((acc, key) => acc?.[key], errors);
  return (
    <Field.Root required={required} invalid={!!fieldError} ref={ref}>
      <Field.Label>{label}<Field.RequiredIndicator /></Field.Label>
      {children}
      <Field.ErrorText>{fieldError?.message}</Field.ErrorText>
      {helpText && <Field.HelpText>{helpText}</Field.HelpText>}
    </Field.Root>
  );
});

const AddButton = forwardRef(({ form, name, schema, ...props }, ref) => {
  const { setValue } = form;
  const newValue = schema.parse({});
  return (
    <Button
      onClick={() => {
        setValue(name, newValue, {
          shouldDirty: true,
          shouldValidate: true,
          shouldTouch: true
        })
      }}
      ref={ref}
      {...props}
    >Add
    </Button>);
});

const DeleteButton = forwardRef(({ form, name, ...props }, ref) => {
  const { setValue } = form;
  const onDelete = () => {
    setValue(name, null, {
      shouldDirty: true,
      shouldValidate: true,
      shouldTouch: true,
    });
  };
  return (
    <DeleteDialog
      trigger={
        <Button
          variant="outline"
          colorPalette="alert"
          ref={ref}
          {...props}
        >Delete
        </Button>
      }
      onDelete={onDelete}
    />);
});

const SaveButton = ({ form, onSubmit }) => {
  const { handleSubmit, formState: { isDirty, isValid } } = form;

  return (<Button size="sm" disabled={!isDirty || !isValid} onClick={handleSubmit(onSubmit)}>Save</Button>);
}

const EditBasicSettings = ({ form }) => {
  const { control, getValues, register } = form;
  const message = getValues();
  return (
    <Fieldset.Root size="md">

      {/* Heading */}
      <Fieldset.Legend>
        <Heading size="lg" mb={0}>Basic Information</Heading>
      </Fieldset.Legend>

      <Separator mt={2} />

      {/* Title */}
      <FormField mb={0} form={form} name="title" label="Title" required>
        <Input
          {...register("title")}
          defaultValue={formatTitle(message?.title)}
        />
      </FormField>

      {/* Description */}
      <FormField mb={0} form={form} name="description" label="Description">
        <TextBox
          {...register("description")}
          defaultValue={formatDescription(message?.description)}
        />
      </FormField>

      {/* Date */}
      <FormField mb={0} form={form} name="date" label="Date" help="Select the date" required>
        <Controller
          control={control}
          name="date"
          render={({ field }) => (
            <DateField
              value={field.value}
              onChange={(date) => field.onChange(date.toISOString())}
            />
          )}
          required
        />
      </FormField>

      {/* Series */}
      <FormField mb={0} form={form} name="seriesId" label="Series" required>
        <Controller
          control={control}
          name="seriesId"
          render={({ field }) => (
            <SeriesComboBox
              value={field.value}
              onValueChange={(e) => {
                const seriesId = (e.value?.length == 1) ? e.value[0] : null;
                field.onChange(seriesId);
              }}
            />
          )}
        />
      </FormField>
    </Fieldset.Root>
  );
};

const EditNotes = ({ form }) => {
  const { getValues, setValue } = form;
  const notes = getValues()?.notes;
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center">
        <Fieldset.Legend>
          <Heading size="lg" mb={0}>Notes</Heading>
        </Fieldset.Legend>
        {notes === null
          ? <AddButton form={form} name="notes" schema={notesSchema} />
          : <DeleteButton form={form} name="notes" />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {notes && <>
        <FormField mb={0} form={form} name="notes.url" label="URL" required>
          <CopyableTextBoxController form={form} name="notes.url" />
        </FormField>
      </>}
    </Fieldset.Root>);
}

const EditAudio = ({ form }) => {
  const { getValues } = form;
  const audio = getValues()?.audio;
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center" verticalAlign="bottom">
        <Fieldset.Legend>
          <Heading size="lg">Audio</Heading>
        </Fieldset.Legend>
        {audio === null
          ? <AddButton form={form} name="audio" schema={audioSchema} />
          : <DeleteButton form={form} name="audio" />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {audio && <>
        {/* Download URL */}
        <FormField mb={0} form={form} name="audio.downloadUrl" label="Download URL" required>
          <CopyableTextBoxController form={form} name="audio.downloadUrl" />
        </FormField>

        {/* Stream URL */}
        <FormField mb={0} form={form} name="audio.streamUrl" label="Stream URL" required>
          <CopyableTextBoxController form={form} name="audio.streamUrl" />
        </FormField>
      </>}
    </Fieldset.Root >
  );
};

const EditVideo = ({ form }) => {
  const { control, getValues, register, formState: { errors } } = form;
  const video = getValues()?.video;
  return (
    <Fieldset.Root size="md">
      {/* Heading */}
      <HStack justify="space-between" align="center" mb={0}>
        <Fieldset.Legend>
          <Heading size="lg" mb={1}>Video</Heading>
        </Fieldset.Legend>
        {video === null
          ? <AddButton form={form} name="video" schema={videoSchema} />
          : <DeleteButton form={form} name="video" />}
      </HStack>

      <Separator mt={2} />

      {/* Fields */}
      {video && <>
        {/* YouTube Video ID */}
        <FormField mb={0} form={form} name="video.youTubeVideoId" label="YouTube Video ID" required>
          <Input
            {...register("video.youTubeVideoId")}
          />
        </FormField>

        {/* Start Time */}
        <FormField mb={0} form={form} name="video.messageStartTimeSeconds" label="Start Time" required>
          <Controller
            control={control}
            name="video.messageStartTimeSeconds"
            render={({ field }) => (
              <DurationInput
                value={field.value}
                onValueChange={(val) => field.onChange(val)}
              />
            )}
          />
        </FormField>
      </>}
    </Fieldset.Root >
  );
};


function MessageForm({ initialData, onSubmit }) {
  const form = useForm({
    resolver: zodResolver(messageSchema),
    defaultValues: initialData ?? {},
    mode: "onChange", // validate on each change
  });
  const { reset } = form;
  return (
    <form>
      <Heading size="2xl" mb={8}>Edit Message</Heading>
      <VStack gap={30} separator={<_StackSeparator />}>

        {/* Basic Information */}
        <EditBasicSettings form={form} />

        {/* Notes */}
        <EditNotes form={form} />

        {/* Audio */}
        <EditAudio form={form} />

        {/* Video */}
        <EditVideo form={form} />

        <Flex width="100%" justifyContent={"flex-end"}>
          <SaveButton form={form} onSubmit={(data) => {
            onSubmit(data);
            reset(data); // New data becomes the default state
          }} />
        </Flex>
      </VStack>
    </form>
  );
}

export function Edit() {
  const [message, setMessage] = useState(null);
  const { id } = useParams();


  const handleSubmit = async (data) => {
    await api.put(`/messages/${id}`, data).then((res) => {
    }).catch((err) => {
      console.error("Error updating message:", err);
    });
  };

  useEffect(() => {
    api.get(`/messages/${id}`).then((res) => {
      setMessage(res.data);
    });
  }, [id]);

  if (!message) {
    return <div>Loading...</div>; // don’t render MessageForm yet
  }

  return (
    <Box p={6} maxW="800px" mx="auto">
      <MessageForm initialData={message} onSubmit={handleSubmit} />
    </Box>
  );
}
